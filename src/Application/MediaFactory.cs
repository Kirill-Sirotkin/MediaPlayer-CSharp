using MediaApp.Business;
using MediaApp.Core;

namespace MediaApp.Application;

class MediaFactory
{
    private string _creatorName;

    public MediaFactory(string creatorName)
    {
        _creatorName = creatorName;
    }

    public Media? CreateMedia(MediaType mediaType, float duration, string name, (int, int)? resolution = null, string? genre = null)
    {
        switch (mediaType)
        {
            case MediaType.Audio:
                return new Audio(duration, Guid.NewGuid(), _creatorName, genre ?? "unknown", name);
            case MediaType.Video:
                return new Video(duration, Guid.NewGuid(), _creatorName, resolution ?? (1920, 1080), name);
        }

        return null;
    }

}