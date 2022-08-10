using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.Colors
{
    public class PetalColorManager : MonoBehaviour
    {
        [SerializeField] private List<Petal> _petals;
        [SerializeField] private float _startDelay;

        private void Start()
        {
            Invoke("ColorPetals", _startDelay);
            GameManager.Instance.ShowEndScreen();
        }

        private void ColorPetals()
        {
            foreach (var petal in _petals)
                petal.ColorPetal();
        }
    }
}
