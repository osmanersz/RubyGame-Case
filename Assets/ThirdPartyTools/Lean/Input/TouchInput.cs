using Game.Scripts.Input;
using UnityEngine;

namespace ThirdPartyTools.Lean.Input
{
    public abstract class TouchInput : MonoBehaviour
    {
        #region Fields

        public bool isActive = false;
        private Vector2 _inputStart;

        #endregion
        #region Virtual Methods

        protected virtual void InputStart(Vector2 newInputValue)
        {
        }
        protected virtual void InputEnd(Vector2 newInputValue)
        {
        }
        #endregion
        #region Private Methods

        private void OnEnable()
        {
            InputController.TouchBegan += OnTouchBegan;
            InputController.Touch += OnTouch;
            InputController.TouchEnded += OnTouchEnded;
            InputController.EnableInput();
            InputController.PopupShow += OnUiPopupShown;
        }

        private void OnDisable()
        {
            InputController.TouchBegan -= OnTouchBegan;
            InputController.Touch -= OnTouch;
            InputController.TouchEnded -= OnTouchEnded;
            InputController.PopupShow -= OnUiPopupShown;
        }

        private void Stop()
        {
            InputEnd(Vector2.zero);
        }

        #region Event Handlers

        private void OnUiPopupShown(bool stat)
        {
            isActive = !stat;
            if (stat)
                Stop();
        }

        private void OnTouchBegan(Vector2 inputPos)
        {
            if (!isActive)
                return;
        
            _inputStart = inputPos;
        }

        private void OnTouch(Vector2 inputPos)
        {
            if (!isActive)
                return;
        
            var inputVector = (inputPos - _inputStart).normalized;
            InputStart(inputVector);
        }

        private void OnTouchEnded(Vector2 inputPos)
        {
            Stop();
        }

        #endregion //Event Handlers

        #endregion //Private Methods
    }
}
