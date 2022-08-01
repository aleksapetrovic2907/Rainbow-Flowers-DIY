using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.Colors
{
    public class ColorsManager : GloballyAccessibleBase<ColorsManager>
    {
        public int CurrentColorIndex = 0;
        public List<Color> PaintColors;

        public void ChangeColor(int index)
        {
            CurrentColorIndex = index;
            EventManager.TriggerEvent(GameEvents.ColorChanged, new Dictionary<string, object> { });
        }

        public static Color FixSaturationAndLightness(Color color, float newSaturation, float newLightness)
        {
            float h, s, v;
            Color.RGBToHSV(color, out h, out s, out v);

            // return Color.HSVToRGB(h, s, newLightness);
            return Color.HSVToRGB(h, newSaturation, newLightness);
        }
    }
}
