using ECSA.SignalR.API.Hubs.Clients;
using Microsoft.AspNetCore.SignalR;

namespace ECSA.SignalR.API.Hubs
{
  public class ChatHub : Hub<IChatClient>
  {
    public async Task SendMessage(ChatMessage message)
    {
      message.User = Context.ConnectionId.ToString();
      await Clients.All.ReceiveMessage(message);
    }
  }
}
