namespace Estudiantes_MVC.Services;

public class ApiUnavailableException : Exception
{
    public ApiUnavailableException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
