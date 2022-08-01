using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class FlowerController : MonoBehaviour
    {
        [SerializeField] private List<Renderer> _petalRenderers;

        private void OnEnable() => EventManager.StartListening(GameEvents.GlassFilled, ChangePetalColors);
        private void OnDisable() => EventManager.StopListening(GameEvents.GlassFilled, ChangePetalColors);

        private void ChangePetalColors(Dictionary<string, object> message)
        {
            foreach (var petal in _petalRenderers)
            {
                var randomColor = GameManager.Instance.FillColors[0];
                petal.material.SetColor("_BaseColor", randomColor);
            }
        }
    }
}
