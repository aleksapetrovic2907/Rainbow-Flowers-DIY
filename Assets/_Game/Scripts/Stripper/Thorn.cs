using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class Thorn : MonoBehaviour
    {
        public bool IsHit = false;

        [SerializeField] private float _strength;

        // direction of the flower normalized
        private Vector3 _direction = new Vector3(-.036f * 3, -1f, -.052f * 2f);

        private Rigidbody _rigidBody;

        private void Start() => _rigidBody = GetComponent<Rigidbody>();

        public void Fall(float speed)
        {
            IsHit = true;

            _rigidBody.isKinematic = false;
            _rigidBody.useGravity = true;
            _rigidBody.AddForce(_direction * _strength * speed, ForceMode.Impulse);
        }
    }
}
