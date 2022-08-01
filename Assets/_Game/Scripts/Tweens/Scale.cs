using UnityEngine;
using DG.Tweening;

namespace Aezakmi.Tweens
{
    public class Scale : TweenBase
    {
        [Header("Scale Tween Settings")]
        [SerializeField] private Vector3 _scaleEnd;

        protected override void SetTweener()
        {
            Tweener = transform
                .DOScale(_scaleEnd, LoopDuration)
                .SetLoops(LoopCount, LoopType)
                .SetEase(LoopEase)
                .SetDelay(Delay);
        }
    }
}