namespace Rocketseat.Exceptions;

public class NotFoundException(string message) : RocketseatException(message)
{
}