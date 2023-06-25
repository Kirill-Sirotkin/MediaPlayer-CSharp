using System.Collections.ObjectModel;

namespace MediaApp;

class MediaLibrary
{
    private static readonly Lazy<MediaLibrary> _instance = new Lazy<MediaLibrary>(() => new MediaLibrary());
    private Playlist _allMedia = new("Media library");
    private List<Playlist> _playlists = new();
    private MediaPlayer _player = MediaPlayer.Instance;

    public Playlist AllMedia
    {
        get => _allMedia;
    }
    public ReadOnlyCollection<Playlist> CustomPlaylists
    {
        get => _playlists.AsReadOnly();
    }

    public static MediaLibrary Instance
    {
        get => _instance.Value;
    }

    public void AddMedia(Media media)
    {
        _allMedia.AddMedia(media);
    }

    public void PrintMedia()
    {
        _allMedia.PrintPlaylist();
    }

    public void AddPlaylist(string name)
    {
        _playlists.Add(new(name));
    }

    public void PrintPlaylists()
    {
        Console.WriteLine("Your playlists:");
        Console.WriteLine($"1. {_allMedia.ToString()}");

        if (_playlists.Count < 1)
        {
            Console.WriteLine("No custom playlists available");
            return;
        }

        for (int i = 0; i < _playlists.Count; i++)
        {
            Console.WriteLine($"{i + 2}. {_playlists[i].ToString()}");
        }
    }

    public void PlayPlaylist(Playlist playlist)
    {
        foreach (Media m in playlist.AllMedia)
        {
            _player.AddMedia(m);
        }
        _player.Play();
    }
}