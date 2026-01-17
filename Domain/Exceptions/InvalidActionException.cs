using System.Globalization;

namespace Domain.Exceptions;

public class InvalidActionException : Exception
{
    public InvalidActionException() : base()
    {
    }

    public InvalidActionException(string message) : base(message)
    {
    }

    public InvalidActionException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}