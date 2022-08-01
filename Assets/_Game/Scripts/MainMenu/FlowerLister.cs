using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Tweens;

namespace Aezakmi
{
    public class FlowerLister : MonoBehaviour
    {
        [SerializeField] private Transform _leftItem, _rightItem, _currentItem;
        [SerializeField] private List<FlowerMainMenu> _flowers;
        private int _currentIndex = 0;

        private void Update()
        {
            if (InputManager.Instance.Touch.phase == TouchPhase.Ended && !InputManager.Instance.IsClickingUI)
            {
                if (InputManager.Instance.SwipeDirection == Swipes.Left)
                    SwipeLeft();
                else if (InputManager.Instance.SwipeDirection == Swipes.Right)
                    SwipeRight();
            }
        }

        private void SwipeLeft()
        {
            _flowers[_currentIndex].SetTargetPosition(_leftItem.position);
            _flowers[_currentIndex].PlayTween();

            _flowers[_currentIndex + 1].SetTargetPosition(_currentItem.position);
            _flowers[_currentIndex + 1].PlayTween();

            if (_currentIndex == _flowers.Count - 1)
                _currentIndex = 0;
            else
                _currentIndex++;

        }

        private void SwipeRight()
        {
            _flowers[_currentIndex].SetTargetPosition(_rightItem.position);
            _flowers[_currentIndex].PlayTween();

            _flowers[_currentIndex -1].SetTargetPosition(_currentItem.position);
            _flowers[_currentIndex -1].PlayTween();

            if (_currentIndex == 0)
                _currentIndex = _flowers.Count - 1;
            else
                _currentIndex++;
        }
    }
}
