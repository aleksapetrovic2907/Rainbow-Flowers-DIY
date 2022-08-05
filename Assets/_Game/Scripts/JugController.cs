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

        [SerializeField] private ParticleSystem ParticleSystem;

        [SerializeField] private Renderer JugRenderer;
        [SerializeField] private float FillWhenStill;
        [SerializeField] private float FillWhenPouring;
        [SerializeField] private float FillChangeSpeed;

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

        private void OnEnable()
        {
            EventManager.StartListening(GameEvents.ColorChanged, ChangeColor);
            EventManager.StartListening(GameEvents.GlassFilled, TurnOffParticles);
            EventManager.StartListening(GameEvents.StepFinished, StepFinished);
            EventManager.StartListening(GameEvents.GlassesPutOnCoasters, delegate { Destroy(gameObject); });
        }
        private void OnDisable()
        {
            EventManager.StopListening(GameEvents.ColorChanged, ChangeColor);
            EventManager.StopListening(GameEvents.GlassFilled, TurnOffParticles);
            EventManager.StopListening(GameEvents.StepFinished, StepFinished);
            EventManager.StopListening(GameEvents.GlassesPutOnCoasters, delegate { Destroy(gameObject); });
        }


        private void StepFinished(Dictionary<string, object> message)
        {
            if (GameManager.Instance.CurrentStep == Steps.ColorMixing)
            {
                foreach (var moveTween in GetComponents<Move>())
                {
                    if (moveTween.tweenTag == "MoveJugUp")
                        moveTween.PlayTween();
                }
            }
        }


        private void Update()
        {
            if (GameManager.Instance.CurrentStep != Steps.ColorMixing)
                return;

            if (InputManager.Instance.IsTouching && !InputManager.Instance.IsClickingUI && ColorMixingManager.Instance.CanPour)
            {
                ParticleSystem.Play();
                _targetRotation = RotationWhenPouring;
                ParticleSystem.enableEmission = true;
                ChangeFill(true);
            }
            else
            {
                _targetRotation = RotationWhenStill;
                ParticleSystem.enableEmission = false;
                ChangeFill(false);
            }

            RotateToTarget();
        }

        private void RotateToTarget()
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(_targetRotation), RotationSpeed * Time.deltaTime);
        }

        private void ChangeFill(bool isPouring)
        {
            var targetFill = 0f;
            targetFill = isPouring ? FillWhenPouring : FillWhenStill;

            JugRenderer.material.SetFloat("_Fill", Mathf.Lerp(JugRenderer.material.GetFloat("_Fill"), targetFill, FillChangeSpeed * Time.deltaTime));
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

        private void TurnOffParticles(Dictionary<string, object> message) => ParticleSystem.Stop();
    }
}
