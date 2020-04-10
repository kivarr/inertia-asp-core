using InertiaCore.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace InertiaCore.Services.Interfaces
{
    public interface IComponentResolver<TCType> where TCType : InertiaController
    {
        string ResolveComponent(string action);
    }
}