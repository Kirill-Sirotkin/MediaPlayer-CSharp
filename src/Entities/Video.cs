namespace MediaApp;

class Video : Media
{
    private int _verticalResolution;
    private int _horizontalResolution;
    private string _type;

    public Video(float duration, Guid id) : base(duration, id)
    {
    }
}