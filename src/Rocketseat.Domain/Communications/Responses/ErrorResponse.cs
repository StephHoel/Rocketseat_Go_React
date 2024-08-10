namespace Rocketseat.Domain.Communications.Responses;

public class ErrorResponse
{
    public IEnumerable<string> Errors { get; set; }

    public ErrorResponse(string message) => Errors = [message];

    public ErrorResponse(IEnumerable<string> messages) => Errors = messages;
}