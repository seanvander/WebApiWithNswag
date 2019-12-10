using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApiWithNswag
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddApiVersioning(options =>
      {
        options.ConfigureControllerVersions();
      })
      .AddVersionedApiExplorer(options =>
      {
        options.SubstitutionFormat = "F";
        options.SubstituteApiVersionInUrl = true;
      });

      services.AddControllers()
      .AddJsonOptions(options =>
      {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.IgnoreNullValues = true;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
      });

      var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

      foreach (var description in provider.ApiVersionDescriptions)
      {
        services.AddOpenApiDocument(document =>
        {
          document.DocumentName = description.GroupName;
          document.Version = description.GroupName;
          document.Title = Assembly.GetExecutingAssembly().GetName().Name;
          document.ApiGroupNames = new[] { description.GroupName };
        });
      }
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      app.UseOpenApi();
      app.UseSwaggerUi3();
      app.UseReDoc(options => options.Path = "/redoc");
    }
  }
}
