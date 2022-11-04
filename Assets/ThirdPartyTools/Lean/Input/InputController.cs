using System;
using Lean.Touch;
using UnityEngine;

namespace Game.Scripts.Input
{
    public enum SwipeDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    public class InputController : MonoBehaviour
    {
        #region Fields

        public static InputController Instance { get; private set; }
        public static bool IsInputEnabled { get; private set; }
        private Vector2 _lastInputPosition;
        private bool _allowOverGuiInput;
        private bool _canSwipe;

        #endregion //Fields

        #region Events

        //Input Events
        public Action<SwipeDirection, float> Swipe;
        public Action<Vector2> Tap;
        public static Action<Vector2> Touch;
        public static Action<Vector2> TouchBegan;
        public Action<Vector2> TouchMoved;
        public static Action<Vector2> TouchEnded;
        public static event Action<bool> PopupShow;

        #endregion //Events

        #region Public Methods

        public void Reset()
        {
            _allowOverGuiInput = false;
            _canSwipe = true;
            _lastInputPosition = Vector2.positiveInfinity;
            Tap = null;
            Touch = null;
            TouchBegan = null;
            TouchMoved = null;
            TouchEnded = null;
        }

        public static void EnableInput()
        {
            IsInputEnabled = true;
        }

        public void DisableInput()
        {
            IsInputEnabled = false;
        }

        public void SetOverGuiInput(bool isAllowed)
        {
            _allowOverGuiInput = isAllowed;
        }

        public Vector2 GetInputPosition()
        {
            if (LeanTouch.Fingers.Count > 0)
                return LeanTouch.Fingers[0].ScreenPosition;    
        
            Debug.LogWarning("No touch! GetInputPosition will return (0,0).");
            return Vector2.zero;
        }

        #endregion //Public Methods

        #region Unity Methods

        private void Awake()
        {
            Singleton();
        }

        #endregion //Unity Methods

        #region Private Methods

        private void Singleton()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
    
        private void OnEnable()
        {
            LeanTouch.OnFingerUpdate += OnFingerUpdate;
            LeanTouch.OnFingerTap += OnFingerTap;
            LeanTouch.OnFingerDown += OnFingerDown;
            LeanTouch.OnFingerUp += OnFingerUp;
            //LeanTouch.OnFingerSwipe += OnFingerSwipe;
        }
    
        private void OnDisable()
        {
            LeanTouch.OnFingerUpdate -= OnFingerUpdate;
            LeanTouch.OnFingerTap -= OnFingerTap;
            LeanTouch.OnFingerDown -= OnFingerDown;
            LeanTouch.OnFingerUp -= OnFingerUp;
            //LeanTouch.OnFingerSwipe -= OnFingerSwipe;
        }
    
        private SwipeDirection GetSwipeDirection(Vector2 swipeDelta)
        {
            //Horizontal Swipe
            if (Math.Abs(swipeDelta.x) > Math.Abs(swipeDelta.y))
                return swipeDelta.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
        
            //Vertical Swipe
            return swipeDelta.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
        }
    
        #region LeanTouch Event Handlers

        private void OnFingerUpdate(LeanFinger finger)
        {
            if (!IsInputEnabled || finger.IsOverGui)
                return;
        
            var inputPosition = finger.ScreenPosition;
            Touch?.Invoke(inputPosition);

            if (!float.IsPositiveInfinity(_lastInputPosition.x))
            {
                if (inputPosition != _lastInputPosition)
                    TouchMoved?.Invoke(inputPosition);
            }
        
            _lastInputPosition = inputPosition;

            if (_canSwipe && finger.SwipeScreenDelta.magnitude * LeanTouch.ScalingFactor > LeanTouch.Instance.SwipeThreshold)
            {
                _canSwipe = false;
                OnFingerSwipe(finger);
            }
        }
    
        private void OnFingerTap(LeanFinger finger)
        {
            if (!IsInputEnabled || !_allowOverGuiInput && finger.IsOverGui)
                return;
        
            Tap?.Invoke(finger.ScreenPosition);
        }
    
        private void OnFingerDown(LeanFinger finger)
        {
            if (!IsInputEnabled || !_allowOverGuiInput && finger.IsOverGui)
                return;
        
            TouchBegan?.Invoke(finger.ScreenPosition);
        }
    
        private void OnFingerUp(LeanFinger finger)
        {
            if (!IsInputEnabled || !_allowOverGuiInput && finger.IsOverGui)
                return;
        
            _canSwipe = true;
            TouchEnded?.Invoke(finger.ScreenPosition);
        }
    
        private void OnFingerSwipe(LeanFinger finger)
        {
            if (!IsInputEnabled || !_allowOverGuiInput && finger.IsOverGui)
                return;
        
            var direction = GetSwipeDirection(finger.SwipeScreenDelta);
            Swipe?.Invoke(direction, finger.SwipeScreenDelta.magnitude);
        }

        public static void OnPopupShow(bool obj)
        {
            PopupShow?.Invoke(obj);
        }

        #endregion //LeanTouch Event Handlers

        #endregion //Private Methods
    }
}