namespace MediaApp;

sealed class MediaPlayer
{
    private static readonly Lazy<MediaPlayer> _instance = new Lazy<MediaPlayer>(() => new MediaPlayer());

    public static MediaPlayer Instance
    {
        get => _instance.Value;
    }
}