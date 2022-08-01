using UnityEngine;

namespace Aezakmi.Colors
{
    public static class ColorMixer
    {
        public static Color32 MixColors(float percentageR, float percentageG, float percentageB)
        {
            byte r = (byte)(255 * percentageR);
            byte g = (byte)(255 * percentageG);
            byte b = (byte)(255 * percentageB);
            return new Color32(r, g, b, 180);
        }
    }
}
