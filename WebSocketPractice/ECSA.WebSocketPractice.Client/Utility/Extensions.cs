using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace ECSA.WebSocketPractice.Client.Utility
{
  public static class Extensions
  {
    public static bool IsLocal(this IWebHostEnvironment env)
    {
      var aspnetcoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
      var dotnetEnvironment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
      return aspnetcoreEnvironment?.ToUpper() == "LOCAL" || dotnetEnvironment?.ToUpper() == "LOCAL";
    }
  }
}
