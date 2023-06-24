namespace MediaApp;

class Audio : Media
{
    private string _genre;

    public Audio(float duration, Guid id, string creatorName, string genre) : base(duration, id, creatorName)
    {
        _genre = genre;
    }
}