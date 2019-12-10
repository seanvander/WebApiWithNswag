using Microsoft.AspNetCore.Mvc;

namespace WebApiWithNswag
{
    public class ApiRouteAttribute : RouteAttribute
    {
        public ApiRouteAttribute(string template = null)
            : base($"api/v{{version:apiVersion}}/{template}")
        {

        }
    }
}
