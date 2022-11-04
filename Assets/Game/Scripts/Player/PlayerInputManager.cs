using Game.Scripts.Interface;
using ThirdPartyTools.Lean.Input;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class PlayerInputManager : TouchInput
    {
        [SerializeField] private ScaleManager _scaleManager;
        [SerializeField] private PlayerPathFollow _pathFollow;
        private bool _isDeath;

        public void GameStart()
        {
            isActive = true;
            _pathFollow.Activate();
        } 
        public void GameEnd()
        {
            isActive = false;
            _pathFollow.Close();
        }

        protected override void InputStart(Vector2 newInputValue)
        {
            if (_isDeath)
                return;
            _scaleManager.SizeUpdate(newInputValue.y);
            if (_scaleManager.CheckCurrentYScaleIsMin())
                _pathFollow.BrakeControl(newInputValue.y);
        }

        protected override void InputEnd(Vector2 newInputValue)
        {
            if (_isDeath)
                return;
            
            base.InputEnd(newInputValue);
            _scaleManager.SizeUpdateEnd();
            _pathFollow.BrakeControl(0);
        }
        public void Kill()
        {
            _isDeath = true;
            _pathFollow.Close();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollectable collectable))
            {
                collectable.Collect();
            }
        }
    }
}
