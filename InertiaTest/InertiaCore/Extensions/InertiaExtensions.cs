using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Humanizer;
using InertiaCore.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace InertiaCore.Extensions
{
    public static class InertiaExtensions
    {
        private const string TempDataModelState = "InertiaTemp";
        private const string TempDataSuccess = "InertiaSuccess";
        private const string TempDataError = "InertiaError";

        public static dynamic GetWithFromTempData(ITempDataDictionary tempData)
        {
            var tempDataGet = tempData.Get<Dictionary<string, string[]>>(TempDataModelState);
            dynamic d = new ExpandoObject();
            d.errors = tempDataGet ?? new Dictionary<string, string[]>();
            var flash = new Dictionary<string, string>();

            var getSuccess = tempData.TryGetValue(TempDataSuccess, out var successMessage);
            flash.Add("success", (string)successMessage);

            var getError = tempData.TryGetValue(TempDataError, out var errorMessage);
            flash.Add("error", (string) errorMessage);
                
            d.flash = flash;

            return d;
        }

        public static Dictionary<string, string[]> SetFromModelStateContext<TType>(
            ModelStateDictionary modelState) where TType : BaseViewModel
        {
            return (from kvp in modelState
                    let field = kvp.Key
                    let state = kvp.Value
                    let errors = state.Errors.Select(e => e.ErrorMessage)
                    where state.Errors.Count > 0
                    select new
                    {
                        Key = typeof(TType).GetProperty(kvp.Key)
                            ?.GetCustomAttribute<JsonPropertyAttribute>() != null ?
                            typeof(TType).GetProperty(kvp.Key)
                                ?.GetCustomAttribute<JsonPropertyAttribute>().PropertyName :   kvp.Key.Underscore(),
                        Errors = errors.ToArray()
                    })
                .ToDictionary(e => e.Key, e => e.Errors);
        }
    }
}