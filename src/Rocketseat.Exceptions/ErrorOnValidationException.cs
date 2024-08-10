using System.Diagnostics.CodeAnalysis;

namespace Rocketseat.Exceptions;

[ExcludeFromCodeCoverage]
public class ErrorOnValidationException : RocketseatException
{
    public ErrorOnValidationException(IEnumerable<string> messages) 
        : base("Multiple errors occurred")
    {
        List<Exception> exceptions = [];

        foreach (var message in messages)
            exceptions.Add(new ErrorOnValidationException(message));

        if (exceptions.Count > 0)
            throw new AggregateException(exceptions);
    }

    public ErrorOnValidationException(string message) : base(message)
    {
    }
}