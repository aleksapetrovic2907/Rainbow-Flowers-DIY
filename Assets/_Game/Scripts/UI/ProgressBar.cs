using UnityEngine;
using UnityEngine.UI;

namespace Aezakmi.UI
{
    public class ProgressBar : MonoBehaviour
    {
        private Slider _slider;

        private void Start() => _slider = GetComponent<Slider>();

        private void LateUpdate()
        {
            // ! TEMP
            if(GameManager.Instance.CurrentStep == Steps.SetupGlasses)
                Destroy(gameObject);
            // ! TEMP

            if (ColorMixingManager.Instance == null)
                return;

            if (ColorMixingManager.Instance.CanPour)
                _slider.value = ColorMixingManager.Instance.CurrentFill;
        }
    }
}
