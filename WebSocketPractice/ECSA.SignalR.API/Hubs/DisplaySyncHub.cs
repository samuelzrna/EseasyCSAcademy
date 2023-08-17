using Microsoft.AspNetCore.SignalR;

namespace ECSA.SignalR.API.Hubs
{
  public class DisplaySyncHub : Hub
  {
    private static int counter = 0;
    private static bool isIncrementing = false;

    public override async Task OnConnectedAsync()
    {
      if (!isIncrementing)
      {
        isIncrementing = true;
        await IncrementCounter();
      }

      await Clients.Caller.SendAsync("UpdateCounter", counter);

      await base.OnConnectedAsync();
    }

    private async Task IncrementCounter()
    {
      while (isIncrementing)
      {
        counter += 1;
        await Task.Delay(10000); // Delay for 10 seconds
        await Clients.All.SendAsync("UpdateCounter", counter);
      }
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
      await base.OnDisconnectedAsync(exception);
    }
  }
}
