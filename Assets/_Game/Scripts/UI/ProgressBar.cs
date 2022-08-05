using UnityEngine;
using UnityEngine.UI;

namespace Aezakmi.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private StripperController StripperController;

        private Slider _slider;

        private void Start() => _slider = GetComponent<Slider>();

        private void LateUpdate()
        {
            if (GameManager.Instance.CurrentStep == Steps.Stripping)
            {
                _slider.value = StripperController.StrippedAmount;
                return;
            }

            if (GameManager.Instance.CurrentStep == Steps.ColorMixing)
            {
                if (ColorMixingManager.Instance == null)
                    return;

                if (ColorMixingManager.Instance.CanPour)
                    _slider.value = ColorMixingManager.Instance.CurrentFill;
            }
        }

        private void OnDisable() => _slider.value = 0f;

    }
}
