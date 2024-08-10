using Microsoft.AspNetCore.Mvc;
using Rocketseat.Application.UseCases.CreateMessage;
using Rocketseat.Application.UseCases.CreateRoom;
using Rocketseat.Application.UseCases.GetRoomMessage;
using Rocketseat.Application.UseCases.GetRoomMessages;
using Rocketseat.Application.UseCases.GetRooms;
using Rocketseat.Application.UseCases.MarkMessageAsAnswered;
using Rocketseat.Application.UseCases.ReactToMessage;
using Rocketseat.Application.UseCases.RemoveReactFromMessage;
using Rocketseat.Domain.Communications.Responses;

namespace Rocketseat.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly ILogger<RoomsController> _logger;

    public RoomsController(ILogger<RoomsController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateRoomResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRoom([FromServices] CreateRoomUseCase useCase,
                                                [FromBody] CreateRoomRequest request)
    {
        _logger.LogInformation($"{nameof(RoomsController)} - {nameof(CreateRoom)}");

        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetRoomsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRooms([FromServices] GetRoomsUseCase useCase)
    {
        _logger.LogInformation($"{nameof(RoomsController)} - {nameof(GetRooms)}");

        var response = await useCase.Execute();

        return Ok(response);
    }

    [HttpGet]
    [Route("{roomId}/messages")]
    [ProducesResponseType(typeof(GetRoomMessagesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRoomMessages([FromServices] GetRoomMessagesUseCase useCase,
                                                     [FromRoute] Guid roomId)
    {
        _logger.LogInformation($"{nameof(RoomsController)} - {nameof(GetRoomMessages)}");

        var response = await useCase.Execute(roomId);

        return Ok(response);
    }

    [HttpPost]
    [Route("{roomId}/messages")]
    [ProducesResponseType(typeof(CreateMessageResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRoomMessage([FromServices] CreateMessageUseCase useCase,
                                                       [FromBody] CreateMessageRequest request,
                                                       [FromRoute] Guid roomId)
    {
        _logger.LogInformation($"{nameof(RoomsController)} - {nameof(CreateRoomMessage)}");

        var response = await useCase.Execute(request, roomId);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [Route("{roomId}/messages/{messageId}")]
    [ProducesResponseType(typeof(GetRoomMessageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRoomMessage([FromServices] GetRoomMessageUseCase useCase,
                                                    [FromRoute] Guid roomId,
                                                    [FromRoute] Guid messageId)
    {
        _logger.LogInformation($"{nameof(RoomsController)} - {nameof(CreateRoomMessage)}");

        var response = await useCase.Execute(roomId, messageId);

        return Ok(response);
    }

    [HttpPatch]
    [Route("{roomId}/messages/{messageId}/react")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ReactToMessage([FromServices] ReactToMessageUseCase useCase,
                                                    [FromRoute] Guid roomId,
                                                    [FromRoute] Guid messageId)
    {
        _logger.LogInformation($"{nameof(RoomsController)} - {nameof(ReactToMessage)}");

        await useCase.Execute(roomId, messageId);

        return NoContent();
    }

    [HttpDelete]
    [Route("{roomId}/messages/{messageId}/react")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveReactFromMessage([FromServices] RemoveReactFromMessageUseCase useCase,
                                                            [FromRoute] Guid roomId,
                                                            [FromRoute] Guid messageId)
    {
        _logger.LogInformation($"{nameof(RoomsController)} - {nameof(RemoveReactFromMessage)}");

        await useCase.Execute(roomId, messageId);

        return NoContent();
    }

    [HttpPatch]
    [Route("{roomId}/messages/{messageId}/answer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MarkMessageAsAnswered([FromServices] MarkMessageAsAnsweredUseCase useCase,
                                                           [FromRoute] Guid roomId,
                                                           [FromRoute] Guid messageId)
    {
        _logger.LogInformation($"{nameof(RoomsController)} - {nameof(MarkMessageAsAnswered)}");

        await useCase.Execute(roomId, messageId);

        return NoContent();
    }
}