using Utils;

namespace MediaApp;

class Audio : Media
{
    private string _genre;

    public Audio(float duration, Guid id, string creatorName, string genre, string name) : base(duration, id, creatorName, name)
    {
        _genre = genre;
    }

    public override string ToString()
    {
        return $"| Audio | {_name} | creator - {_creatorName} | genre - {_genre} | duration - {Converter.SecToMinSec(_durationSec)}";
    }
}