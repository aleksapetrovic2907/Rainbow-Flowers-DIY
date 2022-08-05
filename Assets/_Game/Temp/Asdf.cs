using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class Asdf : MonoBehaviour
    {
        public Color c1, c2, c3;
        private Renderer _renderer;
        public float midstrength;

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            _renderer.material.SetColor("_ColorLeft", c1);
            _renderer.material.SetColor("_ColorMiddle", c2);
            _renderer.material.SetColor("_ColorRight", c3);
            _renderer.material.SetFloat("_MidPower", midstrength);
        }
    }
}
