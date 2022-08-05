using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.Colors
{
    [CreateAssetMenu(fileName = "GradientData", menuName = "RainbowColors/GradientData", order = 1)]
    public class GradientData : ScriptableObject
    {
        public Vector2 LeftKeyTimeMinMax;
        public Vector2 MiddleFirstKeyTimeMinMax;
        public Vector2 MiddleSecondKeyTimeMinMax;
        public Vector2 RightKeyTimeMinMax;

    }
}