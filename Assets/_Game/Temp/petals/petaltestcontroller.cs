using UnityEngine;
using DG.Tweening;

namespace Aezakmi
{
    public class petaltestcontroller : MonoBehaviour
    {
        [SerializeField] private Material _theMaterial;
        [SerializeField] private float _fillAmount;
        [SerializeField] private float _duration;

        private float _fill;
        private Material _material;

        private void Start()
        {
            GetComponent<Renderer>().material = _theMaterial;
            _material = GetComponent<Renderer>().material;
            _fill = _material.GetFloat("Fill");
        }

        public void Color()
        {
            Color randColor = GameManager.Instance.FillColors[Random.Range(0, GameManager.Instance.FillColors.Count)];
            _material.SetColor("PaintColor", randColor);

            DOTween.To(
                                () => _material.GetFloat("Fill"),
                                (val) => _material.SetFloat("Fill", val),
                                _fillAmount,
                                _duration)
                            .SetEase(Ease.OutSine)
                            .Play();
        }
    }
}
