using UnityEngine;
using DG.Tweening;

namespace Aezakmi.Tweens
{
    [RequireComponent(typeof(Camera))]
    public class CameraSize : TweenBase
    {
        [SerializeField] private float _endFov;

        private Camera _camera;
        protected override void Awake()
        {
            _camera = GetComponent<Camera>();
            base.Awake();
        }

        protected override void SetTweener()
        {
            Tweener = _camera
                .DOFieldOfView(_endFov, LoopDuration)
                .SetLoops(LoopCount, LoopType)
                .SetEase(LoopEase)
                .SetDelay(Delay);
        }
    }
}
