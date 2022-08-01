#pragma warning disable
using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Tweens;
using Aezakmi.Colors;

namespace Aezakmi
{
    public class JugController : MonoBehaviour
    {
        [SerializeField] private GlassFillData GlassFillData;

        [SerializeField] private Vector3 RotationWhenStill;
        [SerializeField] private Vector3 RotationWhenPouring;
        [SerializeField] private float RotationSpeed;

        [SerializeField] private Renderer JugRenderer;
        [SerializeField] private ParticleSystem ParticleSystem;
        private ParticleSystem.MainModule _psMain;

        private Vector3 _targetRotation;
        private Scale _scaleTween;
        private Color _fillColor, _topColor, _fresnelColor;

        private void Start()
        {
            _scaleTween = GetComponent<Scale>();
            _psMain = ParticleSystem.main;
            SetColor();
        }

        private void OnEnable() => EventManager.StartListening(GameEvents.ColorChanged, ChangeColor);
        private void OnDisable() => EventManager.StopListening(GameEvents.ColorChanged, ChangeColor);


        private void Update()
        {
            if (InputManager.Instance.IsTouching && !InputManager.Instance.IsClickingUI && ColorMixingManager.Instance.CanPour)
            {
                _targetRotation = RotationWhenPouring;
                ParticleSystem.enableEmission = true;
            }
            else
            {
                _targetRotation = RotationWhenStill;
                ParticleSystem.enableEmission = false;
            }

            RotateToTarget();
        }

        private void RotateToTarget()
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(_targetRotation), RotationSpeed * Time.deltaTime);
        }

        private void ChangeColor(Dictionary<string, object> message)
        {
            _scaleTween.PlayTween();
            SetColor();
        }

        private void SetColor()
        {
            var currentColor = ColorsManager.Instance.PaintColors[ColorsManager.Instance.CurrentColorIndex];

            _fillColor = ColorsManager.FixSaturationAndLightness(currentColor, GlassFillData.JugFillSaturation, GlassFillData.FillLightness);
            _topColor = ColorsManager.FixSaturationAndLightness(_fillColor, GlassFillData.JugTopSaturation, GlassFillData.TopLightness);
            _fresnelColor = ColorsManager.FixSaturationAndLightness(_fillColor, GlassFillData.FresnelSaturation, GlassFillData.FresnelLightness);

            JugRenderer.material.SetColor("_LiquidColor", _fillColor);
            JugRenderer.material.SetColor("_TopColor", _topColor);
            JugRenderer.material.SetColor("_FresnelColor", _fresnelColor);

            _psMain.startColor = ColorsManager.FixSaturationAndLightness(currentColor, GlassFillData.JugTopSaturation, GlassFillData.TopLightness);
        }
    }
}
