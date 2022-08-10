using UnityEngine;

namespace Aezakmi.MainMenu
{
    public class CustomerController : MonoBehaviour
    {
        private Animator _animator;

        private void Start() => _animator = GetComponent<Animator>();
        public void GoIdle() => _animator.SetBool("IsIdle", true);
        public void PopMessage() => MenuManager.Instance.ShowBubble();
    }
}
