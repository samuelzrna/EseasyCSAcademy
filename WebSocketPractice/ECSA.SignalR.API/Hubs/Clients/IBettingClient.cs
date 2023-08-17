using ECSA.SignalR.API.Models;

namespace ECSA.SignalR.API.Hubs.Clients
{
  public interface IBettingClient
  {
    Task ReceiveBet(UserBet userBet);
  }
}
