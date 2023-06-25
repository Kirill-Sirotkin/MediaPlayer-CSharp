using System.Collections.ObjectModel;
using Utils;

namespace MediaApp;

class Playlist
{
    private List<Media> _media = new();
    private string _name;

    public float TotalDuration
    {
        get => _media.Sum(m => m.DurationSec);
    }
    public int MediaCount
    {
        get => _media.Count;
    }
    public ReadOnlyCollection<Media> AllMedia
    {
        get => _media.AsReadOnly();
    }

    public Playlist(string name)
    {
        _name = name;
    }

    public void AddMedia(Media m)
    {
        _media.Add(m);
    }

    public void RemoveMedia(Media m)
    {
        _media.Remove(m);
    }

    public void Shuffle()
    {
        List<Media> shuffled = new();
        Random rnd = new();

        int count = _media.Count;
        for (int i = 0; i < count; i++)
        {
            int index = rnd.Next(0, _media.Count);
            shuffled.Add(_media[index]);
            _media.RemoveAt(index);
        }

        _media = shuffled;
    }

    public void SortByType()
    {
        List<Media> sorted = new();
        foreach (Media m in _media)
        {
            switch (m.GetType().Name)
            {
                case "Audio":
                    sorted.Insert(0, m);
                    break;
                case "Video":
                    sorted.Add(m);
                    break;
            }
        }
        _media = sorted;
    }

    public void SortByLength()
    {
        _media = _media.OrderBy(m => m.DurationSec).ToList();
    }

    public void PrintPlaylist()
    {
        if (_media.Count < 1)
        {
            Console.WriteLine($"Playlist \"{_name}\", empty");
            return;
        }
        Console.WriteLine($"Playlist \"{_name}\", {_media.Count} media:");
        for (int i = 0; i < _media.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_media[i].ToString()}");
        }
    }

    public override string ToString()
    {
        if (_media.Count < 1) return $"| Playlist | name - {_name} | empty";
        return $"| Playlist | name - {_name} | media count - {_media.Count} | total duration - {Converter.SecToMinSec(TotalDuration)}";
    }
}