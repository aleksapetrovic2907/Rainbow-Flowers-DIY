using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Aezakmi.Colors
{
    public enum Positions { TopLeft, TopRight, BottomLeft, BottomRight }
    public class Petal : MonoBehaviour
    {
        [SerializeField] private float _fillAmount;
        [SerializeField] private float _fillDuration;
        [SerializeField] private GradientData _gradientData;
        [SerializeField] private Positions _position;

        private Material _material;

        private Color _colorLeft;
        private Color _colorMiddle;
        private Color _colorRight;

        private void OnEnable() => EventManager.StartListening(GameEvents.GlassesPutOnCoasters, CreateGradient);
        private void OnDisable() => EventManager.StopListening(GameEvents.GlassesPutOnCoasters, CreateGradient);

        private void Start()
        {
            _material = GetComponent<Renderer>().material;
        }

        public void ColorPetal()
        {
            DOTween.To(
                () => _material.GetFloat("_Fill"),
                (val) => _material.SetFloat("_Fill", val),
                _fillAmount,
                _fillDuration)
            .SetEase(Ease.OutSine)
            .OnComplete(ActivateGloss)
            .Play();
        }

        private void CreateGradient(Dictionary<string, object> message)
        {
            GetNearbyColors();

            _material.SetColor("_ColorLeft", FixLightness(_colorLeft));
            _material.SetColor("_ColorMiddle", FixLightness(_colorMiddle));
            _material.SetColor("_ColorRight", FixLightness(_colorRight));
        }

        private void GetNearbyColors()
        {
            _colorMiddle = GameManager.Instance.FillColorsByCoaster[(int)_position];

            if (_position == Positions.TopLeft)
            {
                _colorLeft = GameManager.Instance.FillColorsByCoaster[(int)Positions.TopRight];
                _colorRight = GameManager.Instance.FillColorsByCoaster[(int)Positions.BottomLeft];
            }
            else if (_position == Positions.TopRight)
            {
                _colorLeft = GameManager.Instance.FillColorsByCoaster[(int)Positions.BottomRight];
                _colorRight = GameManager.Instance.FillColorsByCoaster[(int)Positions.TopLeft];
            }
            else if (_position == Positions.BottomLeft)
            {
                _colorLeft = GameManager.Instance.FillColorsByCoaster[(int)Positions.TopLeft];
                _colorRight = GameManager.Instance.FillColorsByCoaster[(int)Positions.BottomRight];
            }
            else if (_position == Positions.BottomRight)
            {
                _colorLeft = GameManager.Instance.FillColorsByCoaster[(int)Positions.BottomLeft];
                _colorRight = GameManager.Instance.FillColorsByCoaster[(int)Positions.TopRight];
            }

        }

        private void ActivateGloss()
        {
            // _material.SetInt("_GlossActive", 1);
        }

        private Color FixLightness(Color color)
        {
            float h, s, v;
            Color.RGBToHSV(color, out h, out s, out v);

            // return Color.HSVToRGB(h, s, newLightness);
            return Color.HSVToRGB(h, 1f, 1f);
        }
    }
}
