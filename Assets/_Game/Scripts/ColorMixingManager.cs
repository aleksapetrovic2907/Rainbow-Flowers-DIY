using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Tweens;

namespace Aezakmi
{
    public class ColorMixingManager : GloballyAccessibleBase<ColorMixingManager>
    {
        public float CurrentFill
        {
            get { return Glasses[_totalGlassesFilled].GetComponent<GlassFillReferencer>().Wobble.fill; }
            private set { }
        }
        public bool CanPour { get; private set; } = false;


        [SerializeField] private List<GameObject> Glasses;

        private int _totalGlassesFilled = 0;

        private void OnEnable() => EventManager.StartListening(GameEvents.GlassFilled, GlassFilled);
        private void OnDisable() => EventManager.StopListening(GameEvents.GlassFilled, GlassFilled);
        public void ChangeCanPour(bool canPour) => CanPour = canPour;

        private void Start()
        {
            foreach (Move moveTween in Glasses[0].GetComponents<Move>())
            {
                if (moveTween.tweenTag == "MoveEmptyGlass")
                    moveTween.PlayTween();
            }
        }

        private void GlassFilled(Dictionary<string, object> message)
        {
            foreach (Move moveTween in Glasses[_totalGlassesFilled].GetComponents<Move>())
            {
                if (moveTween.tweenTag == "MoveFilledGlass")
                {
                    if (_totalGlassesFilled == Glasses.Count - 1)
                    {
                        moveTween.AddDelegateOnComplete(delegate { GameManager.Instance.StepFinished(); });
                        moveTween.PlayTween();
                        return;
                    }

                    moveTween.PlayTween();
                }
            }

            foreach (Move moveTween in Glasses[++_totalGlassesFilled].GetComponents<Move>())
            {
                moveTween.gameObject.SetActive(true);

                if (moveTween.tweenTag == "MoveEmptyGlass")
                    moveTween.PlayTween();
            }
        }
    }
}