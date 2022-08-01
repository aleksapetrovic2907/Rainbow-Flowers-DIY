using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class CoasterController : MonoBehaviour
    {
        private Material _material;

        private void Start() => _material = GetComponent<Renderer>().material;

        public void SetToActive() => _material.SetInt("Fresnel", 1);
        public void SetToInactive() => _material.SetInt("Fresnel", 0);
    }
}
