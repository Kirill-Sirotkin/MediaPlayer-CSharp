namespace MediaApp;

class MediaLibrary
{
    private static readonly Lazy<MediaLibrary> _instance = new Lazy<MediaLibrary>(() => new MediaLibrary());
    private List<Media> _allMedia = new();

    public static MediaLibrary Instance
    {
        get => _instance.Value;
    }
}