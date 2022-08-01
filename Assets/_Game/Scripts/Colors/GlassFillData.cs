using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.Colors
{
    [CreateAssetMenu(fileName = "GlassFillData", menuName = "RainbowColors/GlassFillData", order = 1)]
    public class GlassFillData : ScriptableObject
    {
        [Header("Fill Properties")]
        public float FillSpeed;
        public float FillDelay; // the time for particles to hit the glass

        [Header("Color Properties")]
        public float FillSaturation;
        public float FillLightness;
        [Space(10)]
        public float TopSaturation;
        public float TopLightness;
        [Space(10)]
        public float FresnelSaturation;
        public float FresnelLightness;
        [Space(10)]
        public float JugFillSaturation;
        public float JugTopSaturation;
    }
}
