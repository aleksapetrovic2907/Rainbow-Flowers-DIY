using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Aezakmi
{
    public class GlassFillUI : MonoBehaviour
    {
        [SerializeField] private List<float> FillAmounts;

        [SerializeField] private float FillDuration;
        [SerializeField] private Ease FillEase;

        private int _currentFillIndex = -1;
        private Slider _slider;

        private void Start() => _slider = GetComponent<Slider>();
        private void OnEnable() => EventManager.StartListening(GameEvents.GlassFilled, FillUI);
        private void OnDisable() => EventManager.StopListening(GameEvents.GlassFilled, FillUI);

        private void FillUI(Dictionary<string, object> message)
        {
            DOTween.To(
                () => _slider.value,
                (val) => _slider.value = val,
                FillAmounts[++_currentFillIndex],
                FillDuration)
            .SetEase(FillEase)
            .Play();
        }
    }
}
