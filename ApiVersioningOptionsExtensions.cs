using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using System;
using WebApiWithNswag.v2019_10_01.Controllers;

namespace WebApiWithNswag
{
    public static class ApiVersioningOptionsExtensions
    {
        public static void ConfigureControllerVersions(this ApiVersioningOptions options)
        {
            var version20191001 = new DateTime(2019, 10, 01);

            options.Conventions.Controller<WeatherForecastsController>().HasApiVersion(version20191001);
        }
    }
}
