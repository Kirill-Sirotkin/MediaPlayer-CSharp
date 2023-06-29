using MediaApp.Business;
using MediaApp.Core;

namespace MediaApp.Application;

class MediaNotifier : ISubscriber
{
    private static readonly Lazy<MediaNotifier> _instance = new Lazy<MediaNotifier>(() => new MediaNotifier());

    public static MediaNotifier Instance
    {
        get => _instance.Value;
    }

    public MediaNotifier()
    {
        MediaPlayer.Instance.Subscribe(this);
    }
    ~MediaNotifier()
    {
        MediaPlayer.Instance.Unsubscribe(this);
    }

    public void Receive(string message)
    {
        Console.WriteLine($"(!) | {message}");
    }
}