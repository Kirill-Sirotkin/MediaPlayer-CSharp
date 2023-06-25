using Utils;

namespace MediaApp;

class Media
{
    private Guid _id;
    protected float _durationSec;
    protected float _currentPlace;
    protected string _creatorName;
    protected string _name;

    public float DurationSec
    {
        get => _durationSec;
    }
    public float CurrentPlace
    {
        get => _currentPlace;
    }

    public Media(float duration, Guid id, string creatorName, string name)
    {
        _id = id;
        _creatorName = creatorName;
        _name = name;
        _durationSec = duration;
        _currentPlace = 0f;
    }

    public virtual void Play()
    {
        MediaPlayer.Instance.Notify($"[>] {_name} | duration - {Converter.SecToMinSec(_durationSec)}");
    }
    public virtual void Pause()
    {
        MediaPlayer.Instance.Notify($"[ii] {_name} | duration - {Converter.SecToMinSec(_durationSec)}");
    }
    public virtual void Seek(float place)
    {
        if (place < 0 || place > _durationSec) return; // Seek outside of media duration
        _currentPlace = place;
        MediaPlayer.Instance.Notify($"[>>] {_name} | moving to - {Converter.SecToMinSec(_currentPlace)}");
    }
}