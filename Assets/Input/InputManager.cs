namespace PH.Input
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class InputManager : MonoBehaviour
    {
        private InputActions _input;
        private GestureDetectionSwipe _swipeDetection;
        
        public static Action OnSwipeUp, OnSwipeDown, OnSwipeLeft, OnSwipeRight;
        private InputTouchContact? _touchContactStart;
        private InputTouchContact? _touchContactEnd;

        private void Awake()
        {
            _input = new();
            _swipeDetection = new();
        }

        private void Start()
        {
            _input.Gameplay.TouchContact.started += OnStartTouchContact;
            _input.Gameplay.TouchContact.canceled += OnEndTouchContact;

            OnSwipeUp += () => Debug.Log("OnSwipeUp!");
            OnSwipeDown += () => Debug.Log("OnSwipeDown!");
            OnSwipeLeft += () => Debug.Log("OnSwipeLeft!");
            OnSwipeRight += () => Debug.Log("OnSwipeRight!");
        }

        private void OnDestroy()
        {
            _input.Gameplay.TouchContact.started -= OnStartTouchContact;
            _input.Gameplay.TouchContact.canceled -= OnEndTouchContact;
        }
        
        private void OnEnable() => _input.Enable();

        private void OnDisable() => _input.Disable();

        private void OnStartTouchContact(InputAction.CallbackContext context)
        {
            var position = Touchscreen.current.position.ReadValue();
            var time = (float) context.time;
            _touchContactStart = new(position, time);
        }

        private void OnEndTouchContact(InputAction.CallbackContext context)
        {
            if (!_touchContactStart.HasValue)
                return;
            
            var position = Touchscreen.current.position.ReadValue();
            var time = (float) context.time;
            _touchContactEnd = new(position, time);
            
            var swipeGesture = _swipeDetection.CalculateSwipe(_touchContactStart.Value, _touchContactEnd.Value);
            switch (swipeGesture)
            {
                case GestureDetectionSwipe.SwipeGestures.Invalid:
                    Debug.Log("Invalid Gesture");
                    break;
                case GestureDetectionSwipe.SwipeGestures.SwipeUp:
                    OnSwipeUp?.Invoke();
                    break;
                case GestureDetectionSwipe.SwipeGestures.SwipeDown:
                    OnSwipeDown?.Invoke();
                    break;
                case GestureDetectionSwipe.SwipeGestures.SwipeLeft:
                    OnSwipeLeft?.Invoke();
                    break;
                case GestureDetectionSwipe.SwipeGestures.SwipeRight:
                    OnSwipeRight?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _touchContactStart = null;
            _touchContactEnd = null;
        }
    }
}
