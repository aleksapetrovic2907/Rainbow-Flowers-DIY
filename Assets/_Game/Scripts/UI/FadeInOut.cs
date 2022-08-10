using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Aezakmi.UI
{
    public class FadeInOut : MonoBehaviour
    {
        [SerializeField] private bool FadeOutOnStart;
        [SerializeField] private float FadeDuration;

        private Image _image;

        private void Start()
        {
            _image = GetComponent<Image>();

            if (FadeOutOnStart)
                FadeOut();
        }

        public void FadeOut() => _image.DOFade(0f, FadeDuration).Play();
    }
}
