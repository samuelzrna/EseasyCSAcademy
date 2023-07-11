using Microsoft.AspNetCore.SignalR;

namespace ECSA.SignalR.API.Hubs
{
  public class POSImageHub : Hub
  {
    private readonly Dictionary<string, List<string>> _clientPlaylists = new Dictionary<string, List<string>>();

    public async Task SendMessage(ChatMessage message)
    {
      await Clients.All.SendAsync("ReceiveMessage", message);
    }

    public async Task SetPlaylist(List<string> playlist)
    {
      var connectionId = Context.ConnectionId;
      _clientPlaylists[connectionId] = playlist;
      await Clients.Caller.SendAsync("ReceivePlaylist", playlist);
    }

    public override async Task OnDisconnectedAsync(System.Exception exception)
    {
      var connectionId = Context.ConnectionId;
      if (_clientPlaylists.ContainsKey(connectionId))
      {
        _clientPlaylists.Remove(connectionId);
      }
      await base.OnDisconnectedAsync(exception);
    }

    // Method to retrieve the playlist for a specific client
    public List<string> GetPlaylistForClient(string clientId)
    {
      if (_clientPlaylists.ContainsKey(clientId))
      {
        return _clientPlaylists[clientId];
      }
      return null;
    }

    public async Task<string> ReceiveConnectionId()
    {
      // Get the connection ID for the current client
      string connectionId = Context.ConnectionId;
      await Clients.Caller.SendAsync("ReceiveConnectionId", connectionId);
      return connectionId;
    }

    public override async Task OnConnectedAsync()
    {
      // Get the connection ID for the current client
      string connectionId = Context.ConnectionId;

      // Send the connection ID back to the client
      await Clients.Caller.SendAsync("ReceiveConnectionId", connectionId);

      await base.OnConnectedAsync();
    }
  }

  public class ChatMessage
  {
    public string User { get; set; }

    public string Message { get; set; }
  }
}
