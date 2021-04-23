using System;
using UnityEngine;

namespace UnitySpace.Controllers
{
    public class InputController
    {
        public event Action<Vector2> LongPressed;

        private float _startTouchTime;
        private Vector2 _startTouchPosition;
        private bool _touchStarted;

        public InputController()
        {
            MainController.Instance.Updated += OnUpdated;
        }

        private void OnUpdated()
        {
            CheckLongPress();
        }

        private void CheckLongPress()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    _touchStarted = true;
                    _startTouchPosition = touch.position;
                    _startTouchTime = Time.unscaledTime;
                }

                if (touch.phase == TouchPhase.Ended && _touchStarted)
                {
                    _touchStarted = false;
                    Vector2 deltaPos = touch.position - _startTouchPosition;
                    float deltaTime = Time.unscaledTime - _startTouchTime;

                    bool isLongPressOffset = deltaPos.sqrMagnitude <
                                             Constants.LongPressOffsetTreshhold * Constants.LongPressOffsetTreshhold;
                    bool isLongPressTime = deltaTime < Constants.LongPressMaxTime && deltaTime > Constants.LongPressMinTime;

                    if (isLongPressOffset && isLongPressTime)
                    {
                        Debug.Log("LongPress detected");
                        LongPressed?.Invoke(touch.position);
                    }
                }

                if (touch.phase == TouchPhase.Canceled)
                {
                    _touchStarted = false;
                }
            }
        }

        public void Deinit()
        {
            MainController.Instance.Updated -= OnUpdated;
        }
    }
}