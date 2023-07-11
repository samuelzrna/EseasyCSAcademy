using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class Program
{
  private static async Task Main(string[] args)
  {
    using (ClientWebSocket webSocket = new ClientWebSocket())
    {
      Uri serviceUri = new Uri("ws://localhost:5000/ws");

      try
      {
        await webSocket.ConnectAsync(serviceUri, CancellationToken.None);

        _ = ReceiveWebSocketMessages(webSocket);

        while (webSocket.State == WebSocketState.Open)
        {
          Console.WriteLine("Enter a message to send:");
          string message = Console.ReadLine();

          if (!string.IsNullOrEmpty(message))
          {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
          }
        }
      }
      catch (WebSocketException ex)
      {
        Console.WriteLine($"WebSocket error: {ex.Message}");
      }
    }

    Console.ReadLine();
  }

  private static async Task ReceiveWebSocketMessages(ClientWebSocket webSocket)
  {
    byte[] buffer = new byte[1024];

    while (webSocket.State == WebSocketState.Open)
    {
      WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
      string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
      Console.WriteLine($"Received message: {message}");
    }
  }
}

/*using System.Net.WebSockets;
using System.Text;

namespace ECSA.WebSocketPractice.ClientConsoleApp
{
  class Program
  {
    static async Task Main(string[] args)
    {
      Console.WriteLine("Press Enter");
      Console.ReadLine();

      using (ClientWebSocket client = new ClientWebSocket())
      {
        Uri serivceUri = new Uri("ws://localhost:5237/send");
        var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromSeconds(120));
        try
        {
          await client.ConnectAsync(serivceUri, cts.Token);
          var n = 0;
          while (client.State == WebSocketState.Open)
          {
            Console.WriteLine("enter message to send");
            string message = Console.ReadLine();
            if (!string.IsNullOrEmpty(message)) 
            { 
              ArraySegment<byte> bytesToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
              await client.SendAsync(bytesToSend, WebSocketMessageType.Text, true, cts.Token);
              var responseBuffer = new byte[1024];
              var offset = 0;
              var packet = 1024;
              while (true)
              {
                ArraySegment<byte> byteReceived = new ArraySegment<byte>(responseBuffer, offset, packet); 
                WebSocketReceiveResult response = await client.ReceiveAsync(byteReceived, cts.Token);
                var responseMessage = Encoding.UTF8.GetString(responseBuffer, offset, response.Count);
                Console.WriteLine(responseMessage);
                if (response.EndOfMessage) break;
              }
            }
          }
        }
        catch (WebSocketException ex)
        {
          Console.WriteLine(ex.Message);
        }
      }

      Console.ReadLine();
    }
  }
}*/