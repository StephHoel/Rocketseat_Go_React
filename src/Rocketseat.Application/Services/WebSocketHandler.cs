using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace Rocketseat.Application.Services;

public class WebSocketHandler
{
    private readonly ILogger<WebSocketHandler> _logger;
    private readonly ConcurrentDictionary<Guid, WebSocket> _webSockets = new();

    public WebSocketHandler(ILogger<WebSocketHandler> logger)
    {
        _logger = logger;
    }

    public async Task HandleWebSocketAsync(Guid roomId, WebSocket webSocket)
    {
        _webSockets.TryAdd(roomId, webSocket);
        var buffer = new byte[1024 * 4];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!result.CloseStatus.HasValue)
        {
            _logger.LogInformation("Received message from client");
            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        _logger.LogInformation("Closing WebSocket connection");
        _webSockets.TryRemove(roomId, out _);
        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }

    public async Task SendMessageToRoomAsync(Guid roomId, string message)
    {
        if (_webSockets.TryGetValue(roomId, out var webSocket))
        {
            if (webSocket.State == WebSocketState.Open)
            {
                var messageBuffer = System.Text.Encoding.UTF8.GetBytes(message);
                var messageSegment = new ArraySegment<byte>(messageBuffer);
                await webSocket.SendAsync(messageSegment, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}