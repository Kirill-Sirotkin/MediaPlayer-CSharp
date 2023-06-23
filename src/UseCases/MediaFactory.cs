namespace MediaApp;

class MediaFactory
{
    private string _creatorName;

    public MediaFactory(string creatorName)
    {
        _creatorName = creatorName;
    }

    public Media? CreateMedia(MediaType mediaType, float duration, (int, int)? resolution = null, string? genre = null)
    {
        switch (mediaType)
        {
            case MediaType.Audio:
                return new Audio(duration, Guid.NewGuid(), _creatorName, genre ?? "unknown");
            case MediaType.Video:
                return new Video(duration, Guid.NewGuid(), _creatorName, resolution ?? (1920, 1080));
        }

        return null;
    }

}