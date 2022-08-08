using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class GlassFillCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject ProgressBar;

        private void OnEnable()
        {
            EventManager.StartListening(GameEvents.StepFinished, CheckIfCorrectStep);
        }

        private void OnDisable()
        {
            EventManager.StopListening(GameEvents.StepFinished, CheckIfCorrectStep);
        }

        private void CheckIfCorrectStep(Dictionary<string, object> message)
        {
            bool isCorrectStep = GameManager.Instance.CurrentStep == Steps.ColorMixing;
            ProgressBar.SetActive(isCorrectStep);
        }
    }
}
