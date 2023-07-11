namespace ECSA.SignalR.API.Hubs.Clients
{
  public interface IChatClient
  {
    Task ReceiveMessage(ChatMessage message);
  }
}
