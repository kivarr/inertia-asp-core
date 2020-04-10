using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using InertiaAdapter.Core;
using InertiaCore.Controllers;
using InertiaCore.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InertiaCore.Filters
{
    public class InertiaSaveStateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var result = context;

            if (!context.ActionDescriptor.Parameters.Any(pair 
                => pair.ParameterType.IsSubclassOf(typeof(BaseViewModel)))
            )
                return;
            
            var model = context.ActionDescriptor.Parameters
                .FirstOrDefault(value => value.ParameterType.IsSubclassOf(typeof(BaseViewModel)));

            if (model == null)
                return;

            var testedType = model.ParameterType;
            var mi = typeof(InertiaExtensions).GetMethod("SetFromModelStateContext", new[] { typeof(ModelStateDictionary) });
            var fooRef = mi?.MakeGenericMethod(testedType);
            var objectToTest = fooRef?.Invoke(null, new object[] { context.ModelState });

            if (objectToTest == null)
                return;
            
            var dic = (Dictionary<string, string[]>) objectToTest;
            
            var controller = (InertiaController) context.Controller;
            controller.TempData.Put("InertiaTemp", dic);
            if (dic.Count == 0 && !controller.TempData.ContainsKey("InertiaError"))
                controller.TempData.Add("InertiaSuccess", "Sucesso!");
        }
    }

    public class InertiaGetStateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            if (!context.Controller.GetType().IsSubclassOf(typeof(InertiaController)))
                return;

            var controller = (InertiaController) context.Controller;

            var result = (Result) context.Result;
            var tempData = InertiaExtensions.GetWithFromTempData(controller.TempData);
            context.Result = result.With(tempData);
        }
    }
}