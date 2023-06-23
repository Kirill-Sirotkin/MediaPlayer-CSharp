namespace MediaApp;

class Video : Media
{
    private int _verticalResolution;
    private int _horizontalResolution;

    public Video(float duration, Guid id, string creatorName, (int, int) resolution) : base(duration, id, creatorName)
    {
        _horizontalResolution = resolution.Item1;
        _verticalResolution = resolution.Item2;
    }
}