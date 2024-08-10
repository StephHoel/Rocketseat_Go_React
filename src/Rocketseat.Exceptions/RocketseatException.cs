using System.Diagnostics.CodeAnalysis;

namespace Rocketseat.Exceptions;

[ExcludeFromCodeCoverage]
public class RocketseatException(string message) : SystemException(message)
{
}