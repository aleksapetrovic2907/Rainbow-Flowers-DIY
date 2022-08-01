using System.Collections.Generic;
using UnityEngine;

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
        public Steps CurrentStep { get; private set; } = Steps.IntroAnimation;

        [SerializeField] private ColorMixingManager ColorMixingManager;
        [SerializeField] private GlassesSetupManager GlassesSetupManager;

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

            if (CurrentStep == Steps.ColorMixing)
            {
                ColorMixingManager.gameObject.SetActive(true);
                return;
            }
        }
    }
}
