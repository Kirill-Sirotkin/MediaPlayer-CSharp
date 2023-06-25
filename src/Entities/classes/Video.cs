using Utils;

namespace MediaApp;

class Video : Media
{
    private int _verticalResolution;
    private int _horizontalResolution;

    public Video(float duration, Guid id, string creatorName, (int, int) resolution, string name) : base(duration, id, creatorName, name)
    {
        _horizontalResolution = resolution.Item1;
        _verticalResolution = resolution.Item2;
    }

    public override string ToString()
    {
        return $"| Video | {_name} | creator - {_creatorName} | resolution - {_horizontalResolution}x{_verticalResolution} | duration - {Converter.SecToMinSec(_durationSec)}";
    }
}