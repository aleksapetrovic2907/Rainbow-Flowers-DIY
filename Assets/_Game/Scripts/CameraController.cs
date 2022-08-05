using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Tweens;

namespace Aezakmi
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private List<TweenBase> _tweens;

        private void OnEnable()
        {
            EventManager.StartListening(GameEvents.StepFinished, ChangeCameraSettings);
            EventManager.StartListening(GameEvents.FinishedStripping, delegate { PlayCameraTweens("FinishedStripping"); });
            EventManager.StartListening(GameEvents.GlassesPutOnCoasters, delegate { PlayCameraTweens("StemCutting"); });
            EventManager.StartListening(GameEvents.FinishedStemCutting, delegate { PlayCameraTweens("FinishedStemCutting"); });
        }
        private void OnDisable()
        {
            EventManager.StopListening(GameEvents.StepFinished, ChangeCameraSettings);
            EventManager.StopListening(GameEvents.FinishedStripping, delegate { PlayCameraTweens("FinishedStripping"); });
            EventManager.StopListening(GameEvents.GlassesPutOnCoasters, delegate { PlayCameraTweens("StemCutting"); });
            EventManager.StopListening(GameEvents.FinishedStemCutting, delegate { PlayCameraTweens("FinishedStemCutting"); });
        }

        private void ChangeCameraSettings(Dictionary<string, object> message)
        {
            if (GameManager.Instance.CurrentStep == Steps.SetupGlasses)
                PlayCameraTweens("GlassSetupSettings");
            if (GameManager.Instance.CurrentStep == Steps.PetalColoring)
                PlayCameraTweens("GoToPetalColor");
        }

        private void PlayCameraTweens(string tag)
        {
            foreach (var tween in _tweens)
            {
                if (tween.tweenTag == tag)
                    tween.PlayTween();
            }
        }
    }
}
