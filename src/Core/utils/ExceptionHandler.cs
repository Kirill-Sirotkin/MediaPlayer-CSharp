namespace MediaApp.Core;

class ExceptionHandler : Exception
{
    private string _message;
    private int _errorCode;

    public ExceptionHandler(string message, int errorCode)
    {
        _message = message;
        _errorCode = errorCode;
    }

    private static ExceptionHandler PlaylistException(string message, int errorCode)
    {
        return new ExceptionHandler($"Playlist error: {message}", errorCode);
    }

    public static ExceptionHandler PlaylistEmpty(string? message = null)
    {
        if (message is not null) return ExceptionHandler.PlaylistException($"Playlist was empty: {message}", 1);
        return ExceptionHandler.PlaylistException("Playlist was empty", 1);
    }

    public static ExceptionHandler SeekOutOfBounds(string? message = null)
    {
        if (message is not null) return ExceptionHandler.PlaylistException($"Seek place was out of duration bounds: {message}", 2);
        return ExceptionHandler.PlaylistException("Seek place was out of duration bounds", 2);
    }

    public override string ToString()
    {
        return $"{_message}; Code: {_errorCode}";
    }
}