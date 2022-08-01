using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.Colors
{
    public class GlassFiller : MonoBehaviour
    {
        public bool IsFull;

        [SerializeField] private GlassFillData GlassFillData;

        private float[] _colorFillAmount;
        private Color _fillColor;
        private Color _topColor;
        private Color _fresnelColor;

        private Material _material;
        private Wobble _wobble;

        private const float MAX_FILL_AMOUNT = .99f;

        private void Start()
        {
            _colorFillAmount = new float[3]; // r, g, b

            _material = GetComponent<Renderer>().material;
            _wobble = GetComponent<Wobble>();
        }

        private void Update()
        {
            if (InputManager.Instance.IsTouching && !InputManager.Instance.IsClickingUI && !IsFull && ColorMixingManager.Instance.CanPour)
                Invoke("Fill", GlassFillData.FillDelay);
        }

        private void Fill()
        {
            if (_wobble.fill >= MAX_FILL_AMOUNT)
            {
                _wobble.fill = MAX_FILL_AMOUNT;
                IsFull = true;
                EventManager.TriggerEvent(GameEvents.GlassFilled, new Dictionary<string, object> { { "color", _fillColor } });
                return;
            }

            _wobble.fill += GlassFillData.FillSpeed * Time.deltaTime;
            ChangeColor();
        }

        private void ChangeColor()
        {
            _colorFillAmount[ColorsManager.Instance.CurrentColorIndex] += GlassFillData.FillSpeed * Time.deltaTime;

            _fillColor = ColorMixer.MixColors(_colorFillAmount[0] / _wobble.fill,
                                            _colorFillAmount[1] / _wobble.fill,
                                            _colorFillAmount[2] / _wobble.fill);

            _fillColor = ColorsManager.FixSaturationAndLightness(_fillColor, GlassFillData.FillSaturation, GlassFillData.FillLightness);
            _topColor = ColorsManager.FixSaturationAndLightness(_fillColor, GlassFillData.TopSaturation, GlassFillData.TopLightness);
            _fresnelColor = ColorsManager.FixSaturationAndLightness(_fillColor, GlassFillData.FresnelSaturation, GlassFillData.FresnelLightness);
            _material.SetColor("_LiquidColor", _fillColor);
            _material.SetColor("_TopColor", _topColor);
            _material.SetColor("_FresnelColor", _fresnelColor);
        }

        public void ShowTopOfFill() => _wobble.to1 = .83f;
    }
}
