using UnityEngine;
using DG.Tweening;

namespace Aezakmi.Tweens
{
    public class MoveGlassToCoaster : TweenBase
    {
        [Header("Move Tween Settings")]
        [SerializeField] private MoveType _moveType;
        [SerializeField] private Vector3 _position;
        [SerializeField] private Vector3 _moveAmount;

        private Vector3 _targetPosition;

        protected override void SetTweener()
        {
            return;
        }

        public void MoveGlass(Vector3 position)
        {
            Tweener = transform
                .DOLocalMove(position, LoopDuration)
                .SetLoops(LoopCount, LoopType)
                .SetEase(LoopEase)
                .SetDelay(Delay)
                .Play();
        }

    }
}
