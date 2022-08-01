using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Tweens;

namespace Aezakmi
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private List<TweenBase> _tweens;
        [SerializeField] private List<TweenBase> _tempTweens;
        
        private void OnEnable() => EventManager.StartListening(GameEvents.StepFinished, ChangeCameraSettings);
        private void OnDisable() => EventManager.StopListening(GameEvents.StepFinished, ChangeCameraSettings);

        private void ChangeCameraSettings(Dictionary<string, object> message)
        {
            if(GameManager.Instance.CurrentStep == Steps.SetupGlasses)
                PlayCameraTweens("GlassSetupSettings");
        }

        private void PlayCameraTweens(string tag)
        {
            foreach (var tween in _tweens)
            {
                if(tween.tweenTag == tag)
                    tween.PlayTween();
            }
        }
    }
}
