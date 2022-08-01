using UnityEngine;
using DG.Tweening;

namespace Aezakmi.Tweens
{
    public class Rotate : TweenBase
    {
        [Header("Rotate Tween Settings")]
        [SerializeField] private Vector3 _rotationEnd;
        [SerializeField] private RotateMode _rotateMode;

        protected override void SetTweener()
        {
            Tweener = transform
                .DOLocalRotate(_rotationEnd, LoopDuration, _rotateMode)
                .SetLoops(LoopCount, LoopType)
                .SetEase(LoopEase)
                .SetDelay(Delay);
        }
    }
}