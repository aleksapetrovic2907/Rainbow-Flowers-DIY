using UnityEngine;
using Aezakmi.Tweens;

namespace Aezakmi
{
    public class StripperController : MonoBehaviour
    {
        public bool FinishedStripping { get { return StrippedAmount >= 1f; } private set { } }

        [SerializeField] private float _stripSpeed;
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector3 _endPosition;
        [SerializeField] private Collider _collider;

        [SerializeField] private Move _getOutOfViewportTween;

        public float StrippedAmount { get; private set; } = 0f;
        private float _moveDelta;

        public void Strip()
        {
            if (FinishedStripping)
                return;


            _moveDelta = Mathf.Abs(InputManager.Instance.Touch.deltaPosition.y / Screen.currentResolution.height);

            StrippedAmount += _stripSpeed * Time.deltaTime * _moveDelta;
            transform.position = Vector3.Lerp(_startPosition, _endPosition, StrippedAmount);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<Thorn>() == null)
                return;

            var thorn = other.GetComponent<Thorn>();

            if (thorn.IsHit)
                return;

            other.GetComponent<Thorn>().Fall(_moveDelta);
        }

        public void EnableCollider() => _collider.enabled = true;
        public void MoveOutOfViewport()
        {
            _getOutOfViewportTween.PlayTween();
            Destroy(this);
        }
    }
}
