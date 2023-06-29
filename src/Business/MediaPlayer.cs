using MediaApp.Core;

namespace MediaApp.Business;

sealed class MediaPlayer : IEmitter
{
    private static readonly Lazy<MediaPlayer> _instance = new Lazy<MediaPlayer>(() => new MediaPlayer());
    private Stack<Media> _mediaQueue = new();
    private Media? _currentMedia = null;
    private List<ISubscriber> _subscribers = new();

    public static MediaPlayer Instance
    {
        get => _instance.Value;
    }
    public Media? CurrentMedia
    {
        get => _currentMedia;
    }

    public void AddMedia(Media media)
    {
        _mediaQueue.Push(media);
    }

    public void Play()
    {
        if (_currentMedia is null) _currentMedia = _mediaQueue.Pop();
        _currentMedia.Play();
    }

    public void Pause()
    {
        if (_currentMedia is not null) _currentMedia.Pause();
    }

    public void Skip()
    {
        Pause();
        try
        {
            _currentMedia = _mediaQueue.Pop();
        }
        catch
        {
            throw ExceptionHandler.PlaylistEmpty();
        }
        Play();
    }

    public void Seek(float place)
    {
        if (_currentMedia is not null) _currentMedia.Seek(place);
    }

    public void Clear()
    {
        if (_currentMedia is not null) _currentMedia.Pause();
        _mediaQueue.Clear();
    }

    public void Notify(string message)
    {
        if (_subscribers.Count < 1) return;
        foreach (ISubscriber sub in _subscribers)
        {
            sub.Receive(message);
        }
    }

    public void Subscribe(ISubscriber subscriber)
    {
        _subscribers.Add(subscriber);
    }

    public void Unsubscribe(ISubscriber subscriber)
    {
        _subscribers.Remove(subscriber);
    }
}