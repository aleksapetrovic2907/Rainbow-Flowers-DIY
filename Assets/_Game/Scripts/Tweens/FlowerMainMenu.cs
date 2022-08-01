using UnityEngine;
using DG.Tweening;

namespace Aezakmi.Tweens
{
    public class FlowerMainMenu : TweenBase
    {
        protected override void SetTweener() { }

        public void SetTargetPosition(Vector3 TargetPosition)
        {
            Tweener = transform
                .DOLocalMove(TargetPosition, LoopDuration)
                .SetLoops(LoopCount, LoopType)
                .SetEase(LoopEase)
                .SetDelay(Delay);
        }
    }
}
