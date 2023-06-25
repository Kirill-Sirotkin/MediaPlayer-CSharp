namespace Utils;

static class Converter
{
    public static float MinSecToSec(float minSec)
    {
        int wholePart = (int)MathF.Floor(minSec);
        int decimalPart = (int)(((decimal)minSec % 1) * 100);
        return wholePart * 60 + decimalPart;
    }

    public static float SecToMinSec(float sec)
    {
        float minPart = MathF.Floor(sec / 60);
        float secPart = sec - minPart * 60;
        return minPart + secPart / 100;
    }
}