#pragma warning disable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class BubbleParticleController : MonoBehaviour
    {
        [SerializeField] private Vector2 HeightRange;

        private const float INFINITESIMAL = .01f;
        private ParticleSystem _particleSystem;

        private void OnEnable() => EventManager.StartListening(GameEvents.GlassFilled, ResetHeight);
        private void OnDisable() => EventManager.StartListening(GameEvents.GlassFilled, ResetHeight);
        private void Start() => _particleSystem = GetComponent<ParticleSystem>();

        private void ResetHeight(Dictionary<string, object> message)
        {
            _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);;
            transform.localPosition = new Vector3(transform.localPosition.x, HeightRange.x, transform.localPosition.z);
        }

        private void Update()
        {
            if (GameManager.Instance.CurrentStep != Steps.ColorMixing)
                return;

            if (InputManager.Instance.IsTouching && !InputManager.Instance.IsClickingUI && ColorMixingManager.Instance.CanPour)
            {
                _particleSystem.Play();
                _particleSystem.enableEmission = true;
                MoveUp();
            }
            else
                _particleSystem.enableEmission = false;
        }

        private void MoveUp()
        {
            var targetHeight = Mathf.Lerp(HeightRange.x, HeightRange.y, ColorMixingManager.Instance.CurrentFill);
            transform.localPosition = new Vector3
            (
                transform.localPosition.x,
                targetHeight + INFINITESIMAL,
                transform.localPosition.z
            );
        }
    }
}
