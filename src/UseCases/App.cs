using System.Collections.ObjectModel;
using System.Globalization;
using Utils;

namespace MediaApp;

class App
{
    MediaFactory? _factory;
    MediaPlayer _player = MediaPlayer.Instance;
    MediaLibrary _library = MediaLibrary.Instance;
    MediaNotifier _notifier = MediaNotifier.Instance;

    private static readonly Lazy<App> _instance = new Lazy<App>(() => new App());

    public static App Instance
    {
        get => _instance.Value;
    }

    public void Start()
    {
        bool stopApp = false;

        Console.WriteLine("Welcome to Awesome Media App!");
        Console.Write("Please enter your name: ");
        string? name = Console.ReadLine();
        if (name is null) name = "unknown";

        _factory = new(name);

        Console.WriteLine("Here you can enter commands to control the app. Type \"help\" to see available commands.");
        while (!stopApp)
        {
            Console.Write("App command: ");
            string? command = Console.ReadLine();

            switch (command)
            {
                case "help":
                    PrintCommands();
                    break;
                case "quit":
                    stopApp = true;
                    break;
                case "lib":
                    BrowseLibrary();
                    break;
                default:
                    Console.WriteLine("Unknown command. Type \"help\" to see available commands.");
                    break;
            }
        }

        Console.WriteLine("Thank you for using the app!");
    }

    private void PrintCommands()
    {
        Console.WriteLine("\"help\" - see available commands");
        Console.WriteLine("\"lib\" - start library browsing");
        Console.WriteLine("\"quit\" - stop the application");
    }

    private void PrintLibraryCommands()
    {
        Console.WriteLine("\"help\" - see available commands");
        Console.WriteLine("\"c m\" - start media creation");
        Console.WriteLine("\"m\" - show all media");
        Console.WriteLine("\"pl\" - show all playlists");
        Console.WriteLine("\"b pl\" - browse playlists");
        Console.WriteLine("\"quit\" - quit library browsing");
    }

    private void PrintPlaylistCommands()
    {
        Console.WriteLine("\"help\" - see available commands");
        Console.WriteLine("\"c pl\" - start playlist creation");
        Console.WriteLine("\"m\" - show all media");
        Console.WriteLine("\"pl\" - show all playlists");
        Console.WriteLine("\"play\" - choose a playlist to play");
        Console.WriteLine("\"e\" - edit a playlist");
        Console.WriteLine("\"quit\" - quit playlist browsing");
    }

    private void CreateMedia()
    {
        Console.WriteLine("Welcome to media creation!");
        Console.WriteLine("You can cancel media creation at any time by typing \"quit\".");

        Console.Write("Specify media type (\"audio\" or \"video\"): ");
        bool correctType = false;
        MediaType mediaType = MediaType.Audio;
        while (!correctType)
        {
            string? type = Console.ReadLine();

            switch (type)
            {
                case "audio":
                    correctType = true;
                    break;
                case "video":
                    correctType = true;
                    mediaType = MediaType.Video;
                    break;
                case "a":
                    correctType = true;
                    break;
                case "v":
                    correctType = true;
                    mediaType = MediaType.Video;
                    break;
                case "quit":
                    Console.WriteLine("Cancelling media creation...");
                    return;
                default:
                    Console.WriteLine("Unknown type. Please specify \"audio\" or \"video\".");
                    break;
            }
        }

        Console.Write("Specify media name: ");
        string? mediaName = Console.ReadLine();
        if (mediaName is null) mediaName = "unknown";

        Console.Write("Specify media duration (format: mm.ss): ");
        bool correctDuration = false;
        float duration = 0.1f;
        while (!correctDuration)
        {
            string? dur = Console.ReadLine();
            if (dur is null)
            {
                Console.WriteLine("Please enter a duration in mm.ss:");
                continue;
            };
            if (dur == "quit")
            {
                Console.WriteLine("Cancelling media creation...");
                return;
            }
            try
            {
                CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                culture.NumberFormat.NumberDecimalSeparator = ".";

                duration = float.Parse(dur, culture);
                correctDuration = true;
            }
            catch
            {
                Console.WriteLine("Wrong duration format. Please use mm.ss:");
                continue;
            }
        }

        string genre = "unknown";
        int verticalResolution = 1080;
        int horizontalResolution = 1920;
        switch (mediaType)
        {
            case MediaType.Audio:
                bool correctGenre = false;
                Console.WriteLine("Please choose genre for your audio:");
                Console.WriteLine("\"1\" - Pop");
                Console.WriteLine("\"2\" - HipHop");
                Console.WriteLine("\"3\" - Jazz");
                Console.WriteLine("\"4\" - Metal");
                while (!correctGenre)
                {
                    string? gen = Console.ReadLine();

                    switch (gen)
                    {
                        case "1":
                            correctGenre = true;
                            genre = "Pop";
                            break;
                        case "2":
                            correctGenre = true;
                            genre = "HipHop";
                            break;
                        case "3":
                            correctGenre = true;
                            genre = "Jazz";
                            break;
                        case "4":
                            correctGenre = true;
                            genre = "Metal";
                            break;
                        case "quit":
                            Console.WriteLine("Cancelling media creation...");
                            return;
                        default:
                            Console.WriteLine("Please enter a number from 1 to 4:");
                            break;
                    }
                }
                break;
            case MediaType.Video:
                bool correctRes = false;
                Console.WriteLine("Please choose resolution for your video:");
                Console.WriteLine("\"1\" - 1920x1080");
                Console.WriteLine("\"2\" - 1280x720");
                Console.WriteLine("\"3\" - 854x480");
                Console.WriteLine("\"4\" - 640x360");
                while (!correctRes)
                {
                    string? res = Console.ReadLine();

                    switch (res)
                    {
                        case "1":
                            correctRes = true;
                            verticalResolution = 1080;
                            horizontalResolution = 1920;
                            break;
                        case "2":
                            correctRes = true;
                            verticalResolution = 720;
                            horizontalResolution = 1280;
                            break;
                        case "3":
                            correctRes = true;
                            verticalResolution = 480;
                            horizontalResolution = 854;
                            break;
                        case "4":
                            correctRes = true;
                            verticalResolution = 360;
                            horizontalResolution = 640;
                            break;
                        case "quit":
                            Console.WriteLine("Cancelling media creation...");
                            return;
                        default:
                            Console.WriteLine("Please enter a number from 1 to 4:");
                            break;
                    }
                }
                break;
        }

        Media? media = null;
        if (_factory is not null) media = _factory.CreateMedia
        (
            mediaType,
            Converter.MinSecToSec(duration),
            mediaName,
            (horizontalResolution, verticalResolution),
            genre
        );
        if (media is not null)
        {
            _library.AddMedia(media);
            Console.WriteLine("Media created successfully!");
        }
    }

    private void CreatePlaylist()
    {
        Console.WriteLine("Welcome to playlist creation!");
        Console.WriteLine("You can cancel playlist creation at any time by typing \"quit\".");

        Console.Write("Specify the name for your playlist: ");
        string? name = Console.ReadLine();
        if (name is null) name = "unknown";

        _library.AddPlaylist(name);
        Console.WriteLine("New playlist added!");
    }

    private void BrowseLibrary()
    {
        Console.WriteLine("Welcome to your library! Type \"help\" to see available commands.");

        bool stopLibrary = false;
        while (!stopLibrary)
        {
            Console.Write("Library command: ");
            string? command = Console.ReadLine();

            switch (command)
            {
                case "help":
                    PrintLibraryCommands();
                    break;
                case "quit":
                    stopLibrary = true;
                    break;
                case "c m":
                    CreateMedia();
                    break;
                case "m":
                    _library.PrintMedia();
                    break;
                case "pl":
                    _library.PrintPlaylists();
                    break;
                case "b pl":
                    BrowsePlaylists();
                    break;
                default:
                    Console.WriteLine("Unknown command. Type \"help\" to see available commands.");
                    break;
            }
        }
    }

    private void BrowsePlaylists()
    {
        Console.WriteLine("Welcome to your playlists! Type \"help\" to see available commands.");

        bool stopPlaylists = false;
        while (!stopPlaylists)
        {
            Console.Write("Playlist command: ");
            string? command = Console.ReadLine();

            switch (command)
            {
                case "help":
                    PrintPlaylistCommands();
                    break;
                case "quit":
                    stopPlaylists = true;
                    break;
                case "c pl":
                    CreatePlaylist();
                    break;
                case "m":
                    _library.PrintMedia();
                    break;
                case "pl":
                    _library.PrintPlaylists();
                    break;
                case "play":
                    PlayPlaylist();
                    break;
                case "e":
                    EditPlaylist();
                    break;
                default:
                    Console.WriteLine("Unknown command. Type \"help\" to see available commands.");
                    break;
            }
        }
    }

    private void PrintPlayerCommands()
    {
        Console.WriteLine("\"help\" - see available commands");
        Console.WriteLine("\"ps\" - pause");
        Console.WriteLine("\"pl\" - play");
        Console.WriteLine("\"sk\" - skip to next media");
        Console.WriteLine("\"quit\" - stop player");
    }

    private void PlayPlaylist()
    {
        Playlist? pl = ChoosePlaylist();
        if (pl is null) return;
        Console.WriteLine($"Starting playlist {pl.ToString()}");
        Console.WriteLine("Type \"help\" to see list of available commands");

        _library.PlayPlaylist(pl);

        bool stopPlaylists = false;
        while (!stopPlaylists)
        {
            Console.Write("Player command: ");
            string? command = Console.ReadLine();

            switch (command)
            {
                case "help":
                    PrintPlayerCommands();
                    break;
                case "quit":
                    _player.Pause();
                    stopPlaylists = true;
                    break;
                case "ps":
                    _player.Pause();
                    break;
                case "pl":
                    _player.Play();
                    break;
                case "sk":
                    try
                    {
                        _player.Skip();
                    }
                    catch (Utils.ExceptionHandler e)
                    {
                        Console.WriteLine(e.ToString());
                        stopPlaylists = true;
                    }
                    break;
                default:
                    Console.WriteLine("Unknown command. Type \"help\" to see available commands.");
                    break;
            }
        }
    }

    private Playlist? ChoosePlaylist()
    {
        Console.WriteLine("Choose a playlist by typing its number (type \"quit\" to cancel): ");
        List<Playlist> playlists = new();
        playlists.Add(_library.AllMedia);
        playlists.AddRange(_library.CustomPlaylists);
        for (int i = 0; i < playlists.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {playlists[i].ToString()}");
        }

        bool stopPlaylists = false;
        int index = 0;
        while (!stopPlaylists)
        {
            string? pl = Console.ReadLine();
            if (pl is null)
            {
                Console.WriteLine("Please enter a number or \"quit\"");
                continue;
            }
            if (pl == "quit") return null;
            if (!int.TryParse(pl, out index))
            {
                Console.WriteLine("Please enter a number or \"quit\"");
                continue;
            }
            if (index < 1 || index > playlists.Count)
            {
                Console.WriteLine("Please enter a number corresponding to a playlist");
                continue;
            }
            stopPlaylists = true;
        }

        return playlists[index - 1];
    }

    private void PrintEditCommands()
    {
        Console.WriteLine("\"help\" - see available commands");
        Console.WriteLine("\"add\" - add media to playlist");
        Console.WriteLine("\"rm\" - remove media from playlist");
        Console.WriteLine("\"sh\" - shuffle playlist");
        Console.WriteLine("\"s l\" - sort media in playlist by length (duration)");
        Console.WriteLine("\"s t\" - sort media in playlist by type");
        Console.WriteLine("\"p\" - show media in playlist");
        Console.WriteLine("\"quit\" - quit playlist editing");
    }

    private void EditPlaylist()
    {
        Playlist? pl = ChoosePlaylist();
        if (pl is null) return;

        Console.WriteLine($"Chosen playlist: {pl.ToString()}");
        Console.WriteLine("Type \"help\" to see list of available commands");

        bool stopPlaylists = false;
        while (!stopPlaylists)
        {
            Console.Write("Edit command: ");
            string? command = Console.ReadLine();

            switch (command)
            {
                case "help":
                    PrintEditCommands();
                    break;
                case "quit":
                    stopPlaylists = true;
                    break;
                case "add":
                    AddMediaToPlaylist(pl);
                    break;
                case "rm":
                    RemoveMediaFromPlaylist(pl);
                    break;
                case "sh":
                    pl.Shuffle();
                    break;
                case "s l":
                    pl.SortByLength();
                    break;
                case "s t":
                    pl.SortByType();
                    break;
                case "p":
                    pl.PrintPlaylist();
                    break;
                default:
                    Console.WriteLine("Unknown command. Type \"help\" to see available commands.");
                    break;
            }
        }
    }

    private void AddMediaToPlaylist(Playlist pl)
    {
        ReadOnlyCollection<Media> allMedia = _library.AllMedia.AllMedia;
        if (allMedia.Count < 1)
        {
            Console.WriteLine("No media exists in the library");
            return;
        }

        Console.WriteLine("Choose a media to add by typing its number (type \"quit\" to cancel): ");

        bool stopPlaylists = false;
        int index = 0;
        while (!stopPlaylists)
        {
            string? media = Console.ReadLine();
            if (media is null)
            {
                Console.WriteLine("Please enter a number or \"quit\"");
                continue;
            }
            if (media == "quit") return;
            if (!int.TryParse(media, out index))
            {
                Console.WriteLine("Please enter a number or \"quit\"");
                continue;
            }
            if (index < 1 || index > allMedia.Count)
            {
                Console.WriteLine("Please enter a number corresponding to a media");
                continue;
            }

            pl.AddMedia(allMedia[index - 1]);
            Console.WriteLine("Media added! You can continue, or type \"quit\" to stop adding");
        }
    }

    private void RemoveMediaFromPlaylist(Playlist pl)
    {
        ReadOnlyCollection<Media> allMedia = pl.AllMedia;
        if (allMedia.Count < 1)
        {
            Console.WriteLine("No media exists in the playlist");
            return;
        }

        Console.WriteLine("Choose a media to remove by typing its number (type \"quit\" to cancel): ");
        for (int i = 0; i < allMedia.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {allMedia[i].ToString()}");
        }

        bool stopPlaylists = false;
        int index = 0;
        while (!stopPlaylists)
        {
            allMedia = pl.AllMedia;
            for (int i = 0; i < allMedia.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {allMedia[i].ToString()}");
            }
            string? media = Console.ReadLine();
            if (media is null)
            {
                Console.WriteLine("Please enter a number or \"quit\"");
                continue;
            }
            if (media == "quit") return;
            if (!int.TryParse(media, out index))
            {
                Console.WriteLine("Please enter a number or \"quit\"");
                continue;
            }
            if (index < 1 || index > allMedia.Count)
            {
                Console.WriteLine("Please enter a number corresponding to a media");
                continue;
            }

            pl.RemoveMedia(allMedia[index - 1]);
            Console.WriteLine("Media added! You can continue, or type \"quit\" to stop adding");
        }
    }
}