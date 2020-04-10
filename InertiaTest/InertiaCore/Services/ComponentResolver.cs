using InertiaCore.Controllers;
using InertiaCore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InertiaCore.Services
{
    public class ComponentResolver<TCType> : IComponentResolver<TCType> where TCType : InertiaController
    {
        private const string VUE_COMPONENTS_FOLDER = "Pages";
        private readonly string ControllerName;

        public ComponentResolver()
        {
            ControllerName = typeof(TCType).Name.Replace("Controller", "");
        }
        
        public string ResolveComponent(string action)
        {
            return $"{ControllerName}/{action}.vue";
        }
    }
}