using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class StripperPartMove : MonoBehaviour
    {
        [SerializeField] private float SqueezeSpeed;
        [SerializeField] private Vector3 DefaultRotation;
        [SerializeField] private Vector3 SqueezedRotation;

        private Vector3 _targetRotation;

        private void Awake() => _targetRotation = DefaultRotation;
        private void Update() => transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(_targetRotation), SqueezeSpeed * Time.deltaTime);
        public void Unsqueeze() => _targetRotation = DefaultRotation;
        public void Squeeze() => _targetRotation = SqueezedRotation;
    }
}
