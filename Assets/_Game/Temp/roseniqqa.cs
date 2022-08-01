using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Tweens;

namespace Aezakmi
{
    public class roseniqqa : MonoBehaviour
    {
        [SerializeField] private List<petaltestcontroller> _petals;
        private Move _moveTween;

        private void Start()
        {
            _moveTween = GetComponent<Move>();
        }

        private void OnEnable() => EventManager.StartListening(GameEvents.PutRose, PutRose);
        private void OnDisable() => EventManager.StopListening(GameEvents.PutRose, PutRose);

        private void PutRose(Dictionary<string, object> message)
        {
            _moveTween.AddDelegateOnComplete(ColorPetals);
            _moveTween.PlayTween();
        }

        public void ColorPetals()
        {
            foreach (var petal in _petals)
            {
                petal.Color();
            }
            // Camera.main.GetComponent<CameraController>().ForRose();
        }
    }
}
