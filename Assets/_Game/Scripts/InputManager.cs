using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Swipes
{ Up, Down, Left, Right }

namespace Aezakmi
{
    public class InputManager : GloballyAccessibleBase<InputManager>
    {
        public bool IsTouching { get { return Input.touchCount > 0 || Input.GetMouseButton(0); } private set { } }
        public bool IsClickingUI { get; private set; }
        public Touch Touch { get; private set; }

        public Vector2 StartPosition { get; private set; }
        public Vector2 CurrentPosition { get; private set; }
        public Vector2 EndPosition { get; private set; }

        public Swipes? SwipeDirection { get; private set; } = null;

        private void Update()
        {
            SwipeDirection = null;

            if (Input.touchCount == 0)
                return;

            Touch = Input.touches[0];
            IsClickingUI = IsPointerOverUIObject(Touch);

            if (!IsClickingUI)
            {
                DetectTouchPositions();
            }
        }

        private void DetectTouchPositions()
        {
            CurrentPosition = Touch.position;

            if (Touch.phase == TouchPhase.Began)
                StartPosition = Touch.position;

            if (Touch.phase == TouchPhase.Ended)
                DetectSwipes();

            if (Touch.phase == TouchPhase.Ended)
                EndPosition = Touch.position;
        }

        private void DetectSwipes()
        {
            Vector2 swipeDirection = (EndPosition - StartPosition).normalized;

            float positiveX = Mathf.Abs(swipeDirection.x);
            float positiveY = Mathf.Abs(swipeDirection.y);

            if (positiveX > positiveY)
            {
                SwipeDirection = (swipeDirection.x > 0) ? Swipes.Right : Swipes.Left;
            }
            else
            {
                SwipeDirection = (swipeDirection.y > 0) ? Swipes.Up : Swipes.Down;
            }
        }

        private bool IsPointerOverUIObject(Touch touch)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Touch.position.x, Touch.position.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}
