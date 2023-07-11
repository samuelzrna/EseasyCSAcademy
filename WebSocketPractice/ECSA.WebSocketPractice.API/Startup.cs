using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.OpenApi.Models;
using System.Collections.Concurrent;
using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace ECSA.WebSocketPractice.API
{
  public class Startup
  {
    private readonly WebSocketHandler _webSocketHandler;

    public Startup()
    {
      _webSocketHandler = new WebSocketHandler();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      var webSocketOptions = new WebSocketOptions
      {
        KeepAliveInterval = TimeSpan.FromMinutes(2)
      };

      webSocketOptions.AllowedOrigins.Add("https://localhost:7020");
      webSocketOptions.AllowedOrigins.Add("https://localhost:5000");

      app.UseWebSockets(webSocketOptions);

      app.Map("/ws", HandleWebSocket);

      app.Run(async context =>
      {
        // Handle other HTTP requests
        await context.Response.WriteAsync("Hello, World!");
      });
    }

    private void HandleWebSocket(IApplicationBuilder app)
    {
      app.Use(async (context, next) =>
      {
        if (context.Request.Path == "/ws")
        {
          if (context.WebSockets.IsWebSocketRequest)
          {
            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
            await _webSocketHandler.HandleWebSocketConnection(context, webSocket);
          }
          else
          {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
          }
        }
        else
        {
          await next();
        }
      });
    }
  }
  public class WebSocketHandler
  {
    private readonly ConcurrentDictionary<string, WebSocket> _connectedClients;

    public WebSocketHandler()
    {
      _connectedClients = new ConcurrentDictionary<string, WebSocket>();
    }

    public async Task HandleWebSocketConnection(HttpContext context, WebSocket webSocket)
    {
      string connectionId = Guid.NewGuid().ToString();
      _connectedClients.TryAdd(connectionId, webSocket);

      try
      {
        byte[] buffer = new byte[1024];
        WebSocketReceiveResult result;

        while (webSocket.State == WebSocketState.Open)
        {
          result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

          // Process received message or handle WebSocket logic as needed

          // Example: Echo the received message back to the client
          string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
          await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
        }
      }
      finally
      {
        _connectedClients.TryRemove(connectionId, out _);
        webSocket.Dispose();
      }
    }

    public async Task BroadcastMessage(string message)
    {
      byte[] buffer = Encoding.UTF8.GetBytes(message);

      foreach (var client in _connectedClients)
      {
        if (client.Value.State == WebSocketState.Open)
        {
          await client.Value.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
        }
      }
    }
  }
}





/*
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
      // Adds Microsoft Identity platform (AAD v2.0) support to protect this Api
      
      services.AddControllers();
      services.AddSwaggerGen();

      services.AddCors(o => o.AddPolicy("AllowAllPolicy", builder =>
      {
        builder
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
      }));

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseCors();
      if (env.IsDevelopment() || env.IsStaging())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IBC.MemberPortal.Apps.Web.API v1"));
      }
      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseAuthorization();

      app.UseWebSockets();
      app.Use(async (context, next) =>
      {
          if (context.Request.Path == "/send")
        {
          if (context.WebSockets.IsWebSocketRequest)
          {
            using (WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync())
            {
              await Send(context, webSocket);
            }
          }
          else
          {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
          }
        }

        await next(context);
      });


      try
      {
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        throw;
      }
    }

    private async Task Send(HttpContext context, WebSocket webSocket)
    {
      var buffer = new byte[1024 * 4];
      WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);  
      if (result != null)
      {
        while (!result.CloseStatus.HasValue)
        {
          string msg = Encoding.UTF8.GetString(new ArraySegment<byte>(buffer, 0, result.Count));
          Console.WriteLine($"Client says: {msg}");
          await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes($"{DateTime.UtcNow:f}")), result.MessageType, result.EndOfMessage, CancellationToken.None);
          result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
          // Console.WriteLine(result);
        }
      }
      await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }
  }
}
*/