using System.Net.WebSockets;
using System.Net;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.OpenApi.Models;
using System.Net.Http.Headers;
using Microsoft.Identity.Web;
using ECSA.SignalR.API.Hubs;
using System.Threading;

namespace ECSA.SignalR.API
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
      services.AddSignalR();
      services.AddSingleton<ClockHub>();
      services.AddSingleton<POSImageHub>();
      services.AddSingleton<ChatHub>();
      services.AddSingleton<DisplaySyncHub>();
      // internal layer dependency injection
      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "IBC.DigitalSignage.Apps.Web.API", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      
      app.UseDeveloperExceptionPage();
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IBC.MemberPortal.Apps.Web.API v1"));
      app.UseHttpsRedirection();
      app.UseRouting();

      app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) // allow any origin
        .AllowCredentials()); // allow credentials

      try
      {
        app.UseEndpoints(endpoints => { 
          endpoints.MapControllers();
          endpoints.MapHub<ClockHub>("/clockhub");
          endpoints.MapHub<POSImageHub>("/posImageHub");
          endpoints.MapHub<ChatHub>("/hubs/chat");
          endpoints.MapHub<DisplaySyncHub>("/displaySync");
        });
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        throw;
      }

    }
  }
}
