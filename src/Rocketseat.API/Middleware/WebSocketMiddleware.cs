using Microsoft.Extensions.Localization;
using Rocketseat.Application.Services;
using Rocketseat.Domain.Shared;
using Serilog;
using System.Net;

namespace Rocketseat.API.Middleware;

public class WebSocketMiddleware
{
    private readonly RequestDelegate _next;
    private readonly WebSocketHandler _webSocketHandler;
    private readonly ILogger<WebSocketMiddleware> _logger;
    private readonly IStringLocalizer<ErrorMessages> _localizer;

    public WebSocketMiddleware(RequestDelegate next,
                               WebSocketHandler webSocketHandler,
                               ILogger<WebSocketMiddleware> logger,
                               IStringLocalizer<ErrorMessages> localizer)
    {
        _next = next;
        _webSocketHandler = webSocketHandler;
        _logger = logger;
        _localizer = localizer;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.WebSockets.IsWebSocketRequest || 
            !context.Request.Path.StartsWithSegments("/subscribe"))
        {
            await _next(context);
            return;
        }

        var roomId = context.Request.Path.Value!.Split('/').Last();

        if (!Guid.TryParse(roomId, out Guid roomIdGuid))
        {
            _logger.LogWarning("Invalid room ID format");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(_localizer["InvalidRoom"]);
            return;
        }
        
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        _logger.LogInformation($"WebSocket connection established for room {roomIdGuid}");

        await _webSocketHandler.HandleWebSocketAsync(roomIdGuid, webSocket);
        
    }
}