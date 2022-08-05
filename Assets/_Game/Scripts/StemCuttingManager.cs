using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Aezakmi.Tweens;

namespace Aezakmi
{
    public class StemCuttingManager : GloballyAccessibleBase<StemCuttingManager>
    {
        [SerializeField] private List<SkinnedMeshRenderer> _flowerBottomParts;

        [Header("Knife Settings")]
        [SerializeField] private Move _knifeMoveToFlower;
        [SerializeField] private Rotate _knifeRotateToFlower;
        [SerializeField] private Move _secondKnifeMoveToFlower;
        [SerializeField] private float _delayOffset; // the time the shapes start blending before knife finishes cut
        [SerializeField] private KnifeCut _firstCut;
        [SerializeField] private KnifeCut _secondCut;

        [SerializeField] private Ease _cutEase;
        [Header("First Cut")]
        [SerializeField] private float _firstCutDuration;
        [SerializeField][Range(0f, 100f)] private float _firstCutAmount;
        [Header("Second Cut")]
        [SerializeField] private float _secondCutDuration;
        [SerializeField][Range(0f, 100f)] private float _secondCutAmount;

        private bool _finishedFirstCut = false;

        private bool _canDoFirstCut;
        private bool _canDoSecondCut;

        private void Start()
        {
            _knifeMoveToFlower.AddDelegateOnComplete(delegate { _canDoFirstCut = true; });
            _knifeMoveToFlower.PlayTween();
            _knifeRotateToFlower.PlayTween();
        }

        private void Update()
        {
            if (_canDoFirstCut && InputManager.Instance.SwipeDirection == Swipes.Down && InputManager.Instance.Touch.phase == TouchPhase.Ended)
            {
                if (!_finishedFirstCut)
                {
                    TweenFirstCut();
                    _firstCut.AddDelegateOnComplete(delegate { EventManager.TriggerEvent(GameEvents.StemCut, new Dictionary<string, object> { }); });
                    _firstCut.PlayTween();
                    _finishedFirstCut = true;
                }

                if (_canDoSecondCut)
                {
                    _canDoSecondCut = false;
                    _secondCut.PlayTween();
                    TweenSecondCut();
                }
            }
        }

        private void TweenFirstCut()
        {
            // NE and SW
            DOTween.To(
                () => _flowerBottomParts[0].GetBlendShapeWeight(1),
                (val) => _flowerBottomParts[0].SetBlendShapeWeight(1, val),
                _firstCutAmount,
                _firstCutDuration)
            .SetEase(_cutEase)
            .SetDelay(_firstCut.CutDuration - _delayOffset)
            .Play();

            DOTween.To(
                () => _flowerBottomParts[1].GetBlendShapeWeight(1),
                (val) => _flowerBottomParts[1].SetBlendShapeWeight(1, val),
                _firstCutAmount,
                _firstCutDuration)
            .SetEase(_cutEase)
            .SetDelay(_firstCut.CutDuration - _delayOffset)
            .Play();

            // NW and SE
            DOTween.To(
                () => _flowerBottomParts[2].GetBlendShapeWeight(0),
                (val) => _flowerBottomParts[2].SetBlendShapeWeight(0, val),
                _firstCutAmount,
                _firstCutDuration)
            .SetEase(_cutEase)
            .SetDelay(_firstCut.CutDuration - _delayOffset)
            .Play();

            DOTween.To(
                () => _flowerBottomParts[3].GetBlendShapeWeight(0),
                (val) => _flowerBottomParts[3].SetBlendShapeWeight(0, val),
                _firstCutAmount,
                _firstCutDuration)
            .SetEase(_cutEase)
            .SetDelay(_firstCut.CutDuration - _delayOffset)
            .Play();
        }

        private void TweenSecondCut()
        {
            // NE and SW
            DOTween.To(
                () => _flowerBottomParts[0].GetBlendShapeWeight(1),
                (val) => _flowerBottomParts[0].SetBlendShapeWeight(1, val),
                _secondCutAmount,
                _secondCutDuration)
            .SetEase(_cutEase)
            .SetDelay(_firstCut.CutDuration - _delayOffset)
            .Play();

            DOTween.To(
                () => _flowerBottomParts[0].GetBlendShapeWeight(0),
                (val) => _flowerBottomParts[0].SetBlendShapeWeight(0, val),
                _secondCutAmount,
                _secondCutDuration)
            .SetEase(_cutEase)
            .SetDelay(_firstCut.CutDuration - _delayOffset)
            .Play();

            DOTween.To(
                () => _flowerBottomParts[1].GetBlendShapeWeight(1),
                (val) => _flowerBottomParts[1].SetBlendShapeWeight(1, val),
                _secondCutAmount,
                _secondCutDuration)
            .SetEase(_cutEase)
            .SetDelay(_firstCut.CutDuration - _delayOffset)
            .Play();

            DOTween.To(
                () => _flowerBottomParts[1].GetBlendShapeWeight(0),
                (val) => _flowerBottomParts[1].SetBlendShapeWeight(0, val),
                _secondCutAmount,
                _secondCutDuration)
            .SetEase(_cutEase)
            .SetDelay(_firstCut.CutDuration - _delayOffset)
            .Play();

            // NW and SE
            DOTween.To(
                () => _flowerBottomParts[2].GetBlendShapeWeight(0),
                (val) => _flowerBottomParts[2].SetBlendShapeWeight(0, val),
                _secondCutAmount,
                _secondCutDuration)
            .SetEase(_cutEase)
            .SetDelay(_firstCut.CutDuration - _delayOffset)
            .Play();

            DOTween.To(
                () => _flowerBottomParts[2].GetBlendShapeWeight(1),
                (val) => _flowerBottomParts[2].SetBlendShapeWeight(1, val),
                _secondCutAmount,
                _secondCutDuration)
            .SetEase(_cutEase)
            .SetDelay(_firstCut.CutDuration - _delayOffset)
            .Play();

            DOTween.To(
                () => _flowerBottomParts[3].GetBlendShapeWeight(0),
                (val) => _flowerBottomParts[3].SetBlendShapeWeight(0, val),
                _secondCutAmount,
                _secondCutDuration)
            .SetEase(_cutEase)
            .SetDelay(_firstCut.CutDuration - _delayOffset)
            .Play();

            DOTween.To(
                () => _flowerBottomParts[3].GetBlendShapeWeight(1),
                (val) => _flowerBottomParts[3].SetBlendShapeWeight(1, val),
                _secondCutAmount,
                _secondCutDuration)
            .SetEase(_cutEase)
            .SetDelay(_firstCut.CutDuration - _delayOffset)
            .OnComplete(FinishCutting) // ! ON COMPLETE HERE
            .Play();
        }

        public void TweenKnifeForSecondCut()
        {
            _secondKnifeMoveToFlower.AddDelegateOnComplete(delegate { _canDoSecondCut = true; });
            _secondKnifeMoveToFlower.PlayTween();
        }

        private void FinishCutting()
        {
            EventManager.TriggerEvent(GameEvents.FinishedStemCutting, new Dictionary<string, object> { });
        }
    }
}

/*
    first two elements are NE and SW, second two are NW AND SE

    first cut explanation:
    NE and SW use second blendshape [1]
    NW AND SE use first blendshape [0]

    second cut is opposite.
*/
