using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.UI
{
    public class ColorsCanvas : MonoBehaviour
    {
        private void OnEnable() => EventManager.StartListening(GameEvents.StepFinished, ToggleActive);
        private void OnDisable() => EventManager.StartListening(GameEvents.StepFinished, ToggleActive);

        private void ToggleActive(Dictionary<string, object> message)
        {
            if (GameManager.Instance.CurrentStep == Steps.SetupGlasses)
                gameObject.SetActive(false);
        }
    }
}
