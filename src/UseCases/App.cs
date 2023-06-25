using System.Globalization;

namespace MediaApp;

class App
{
    MediaFactory? factory;
    MediaPlayer player = MediaPlayer.Instance;
    MediaLibrary library = MediaLibrary.Instance;

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

        factory = new(name);

        Console.WriteLine("Here you can enter commands to control the app. Type \"help\" to see available commands.");
        while (!stopApp)
        {
            Console.Write("Your command: ");
            string? command = Console.ReadLine();

            switch (command)
            {
                case "help":
                    PrintCommands();
                    break;
                case "quit":
                    stopApp = true;
                    break;
                case "create":
                    CreateMedia();
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
        Console.WriteLine("\"create\" - start media creation");
        Console.WriteLine("\"quit\" - stop the application");
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
                case "quit":
                    Console.WriteLine("Cancelling media creation...");
                    return;
                default:
                    Console.WriteLine("Unknown type. Please specify \"audio\" or \"video\".");
                    break;
            }
        }

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
        if (factory is not null) media = factory.CreateMedia(mediaType, duration, (horizontalResolution, verticalResolution), genre);
    }
}