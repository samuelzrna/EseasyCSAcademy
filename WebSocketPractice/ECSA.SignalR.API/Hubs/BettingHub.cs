using ECSA.SignalR.API.Hubs.Clients;
using Microsoft.AspNetCore.SignalR;

namespace ECSA.SignalR.API.Hubs
{
  public class BettingHub: Hub<IBettingClient>
  {
  }
}
