using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Tweens;

namespace Aezakmi
{
    public class FlowerMovementController : MonoBehaviour
    {
        [SerializeField] private List<TweenBase> _tweens;
        [SerializeField] private Vector3 _cuttingPosition = new Vector3(6.30999994f, 0.0299999993f, -4.4000001f);

        private void OnEnable()
        {
            EventManager.StartListening(GameEvents.FinishedStripping, FinishedStripping);
            EventManager.StartListening(GameEvents.StemCut, delegate { MoveFlower("RotateFlowerForSecondCut"); });
            EventManager.StartListening(GameEvents.FinishedStemCutting, delegate { MoveFlower("FinishedStemCutting"); });
            EventManager.StartListening(GameEvents.StepFinished, DipInGlass);
        }

        private void OnDisable()
        {
            EventManager.StopListening(GameEvents.FinishedStripping, FinishedStripping);
            EventManager.StopListening(GameEvents.StemCut, delegate { MoveFlower("RotateFlowerForSecondCut"); });
            EventManager.StopListening(GameEvents.FinishedStemCutting, delegate { MoveFlower("FinishedStemCutting"); });
        }


        private void FinishedStripping(Dictionary<string, object> message)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var go = gameObject.transform.GetChild(i);

                if (go.tag == "Disposable")
                    go.parent = null;
            }

            MoveFlower("FinishedStripping");
        }

        private void MoveFlower(string tag)
        {
            foreach (var tween in _tweens)
            {
                if (tween.tweenTag == tag)
                    tween.PlayTween();
            }
        }

        private void DipInGlass(Dictionary<string, object> message)
        {
            if(GameManager.Instance.CurrentStep == Steps.PetalColoring)
                MoveFlower("DipInGlass");
        }

        public void TeleportForCutting() => transform.position = _cuttingPosition;
    }
}
