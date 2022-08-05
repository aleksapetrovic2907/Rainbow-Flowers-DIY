using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Tweens;

namespace Aezakmi
{
    public class StrippingManager : MonoBehaviour
    {
        [SerializeField] private List<TweenBase> _stripperMoveToRoseTweens;
        [SerializeField] private StripperController _stripperController;
        [SerializeField] private List<StripperPartMove> _stripperParts;

        private bool _canStrip;

        private void Start()
        {
            _stripperMoveToRoseTweens[0].AddDelegateOnComplete(OnStripperInPosition);

            foreach (var tween in _stripperMoveToRoseTweens)
                tween.PlayTween();
        }

        private void OnStripperInPosition()
        {
            _canStrip = true;
            _stripperController.EnableCollider();
        }

        private void Update()
        {
            if (_stripperController.FinishedStripping)
            {
                foreach (var part in _stripperParts)
                    part.Unsqueeze();

                _stripperController.MoveOutOfViewport();
                EventManager.TriggerEvent(GameEvents.FinishedStripping, new Dictionary<string, object> { });
                this.enabled = false;
                return;
            }

            if (!_canStrip || !InputManager.Instance.IsTouching || InputManager.Instance.IsClickingUI)
            {
                foreach (var part in _stripperParts)
                    part.Unsqueeze();

                return;
            }

            foreach (var part in _stripperParts)
                part.Squeeze();

            if (InputManager.Instance.Touch.phase == TouchPhase.Moved && InputManager.Instance.Touch.deltaPosition.y < 0f)
            {
                _stripperController.Strip();
            }
        }
    }
}
