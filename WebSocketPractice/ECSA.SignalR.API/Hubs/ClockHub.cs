using Microsoft.AspNetCore.SignalR;

namespace ECSA.SignalR.API.Hubs
{
  public class ClockHub : Hub
  {
    private Timer _timer;

    public override async Task OnConnectedAsync()
    {

      StartSendingTime();

      await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
      StopSendingTime();

      return base.OnDisconnectedAsync(exception);
    }

    private void StartSendingTime()
    {
      _timer = new Timer(state =>
      {
        var currentTime = DateTime.UtcNow.ToString("HH:mm:ss");
        Clients.All.SendAsync("ReceiveTime", currentTime);
      }, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }

    private void StopSendingTime()
    {
      _timer?.Dispose();
      _timer = null;
    }
  }
}
