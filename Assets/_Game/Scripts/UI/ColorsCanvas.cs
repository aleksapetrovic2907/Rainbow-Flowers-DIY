using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.UI
{
    public class ColorsCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject Colors;
        private void OnEnable() => EventManager.StartListening(GameEvents.StepFinished, ToggleActive);
        private void OnDisable() => EventManager.StartListening(GameEvents.StepFinished, ToggleActive);

        private void ToggleActive(Dictionary<string, object> message)
        {
            if (GameManager.Instance.CurrentStep == Steps.ColorMixing)
                ToggleColors(true);
            else
                ToggleColors(false);
        }

        public void ToggleColors(bool active) => Colors.SetActive(active);
    }
}
