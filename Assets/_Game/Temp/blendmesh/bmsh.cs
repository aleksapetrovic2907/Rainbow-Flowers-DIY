using UnityEngine;
using DG.Tweening;

namespace Aezakmi
{
    public class bmsh : MonoBehaviour
    {

        [SerializeField] private float FirstBlendDuration;
        [SerializeField] private float SecondBlendDuration;

        private Tweener _tweener;
        private SkinnedMeshRenderer _skinnedMeshRenderer;

        private void Start()
        {
            _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
            StartBlendShape();
        }

        private void StartBlendShape()
        {
            _tweener = DOTween.To(
                () => _skinnedMeshRenderer.GetBlendShapeWeight(0),
                x => _skinnedMeshRenderer.SetBlendShapeWeight(0, x),
                100,
                FirstBlendDuration)
                .SetEase(Ease.OutSine)
                .SetDelay(.5f)
                .Play();
        }
    }
}
