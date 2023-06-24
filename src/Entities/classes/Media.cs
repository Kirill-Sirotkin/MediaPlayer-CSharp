namespace MediaApp;

class Media
{
    private Guid _id;
    private float _durationSec;
    private float _currentPlace;
    private string _creatorName;

    public Media(float duration, Guid id, string creatorName)
    {
        _id = id;
        _creatorName = creatorName;
        _durationSec = duration;
        _currentPlace = 0f;
    }

    public virtual void Play() { }
    public virtual void Pause() { }
    public virtual void Seek(float place)
    {
        if (place < 0 || place > _durationSec) return; // Seek outside of media duration

    }
}