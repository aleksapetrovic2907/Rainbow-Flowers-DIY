using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Colors;

public enum Steps
{
    IntroAnimation,
    Stripping,
    ColorMixing,
    SetupGlasses,
    StemCutting,
    PetalColoring,
    EndingAnimation
}

namespace Aezakmi
{
    public class GameManager : GloballyAccessibleBase<GameManager>
    {
        public GameObject CurrentFill;
        public List<Color> FillColors { get; private set; } = new List<Color>();
        public List<Color> FillColorsByCoaster = new List<Color>(); // so we can create a gradient of nearby glasses' colors
        public Steps CurrentStep { get; private set; } = Steps.IntroAnimation;

        [SerializeField] private StrippingManager StrippingManager;
        [SerializeField] private ColorMixingManager ColorMixingManager;
        [SerializeField] private StemCuttingManager StemCuttingManager;
        [SerializeField] private PetalColorManager PetalColorManager;

        private void OnEnable()
        {
            EventManager.StartListening(GameEvents.GlassFilled, AddNewColor);
        }

        private void OnDisable()
        {
            EventManager.StopListening(GameEvents.GlassFilled, AddNewColor);
        }

        private void AddNewColor(Dictionary<string, object> message)
        {
            FillColors.Add((Color)message["color"]);
        }

        public void StepFinished()
        {
            CurrentStep++;
            EventManager.TriggerEvent(GameEvents.StepFinished, new Dictionary<string, object> { });

            if (CurrentStep == Steps.Stripping)
            {
                StrippingManager.gameObject.SetActive(true);
                return;
            }

            if (CurrentStep == Steps.ColorMixing)
            {
                ColorMixingManager.gameObject.SetActive(true);
                return;
            }

            if (CurrentStep == Steps.StemCutting)
            {
                StemCuttingManager.gameObject.SetActive(true);
                return;
            }

            if(CurrentStep == Steps.PetalColoring)
            {
                PetalColorManager.gameObject.SetActive(true);
                return;
            }
        }
    }
}
