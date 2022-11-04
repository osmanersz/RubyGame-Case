using System.Collections;
using PathCreator;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class PlayerPathFollow : PathFollow
    {
        [SerializeField] private Transform mesh;
        [SerializeField] private float _maxSpeed=8;
        private bool _isTouching;
        private bool _isBraking;
        private bool _canDamage;

        public void BrakeControl(float input)
        {
            if (input < 0)
            {
                _isBraking = true;
            }
            else _isBraking = false;
        }

        protected override void FixedUpdate()
        {
            if (!_isActive)
                return;

            mesh.Rotate(new Vector3(currentSpeed*20 * Time.deltaTime, 0, 0)); 
            base.FixedUpdate();
            if (_isBraking)
            {
                if (currentSpeed > 0.1f)
                    currentSpeed -= _maxSpeed * 1.5f * Time.fixedDeltaTime;
            }
            else if (currentSpeed < _maxSpeed)
                currentSpeed += _maxSpeed * Time.fixedDeltaTime;
        }

        public void Hit()
        {
            if (_canDamage) return;
            _canDamage = true;
            StartCoroutine(HitIe());
        }

        private IEnumerator HitIe()
        {
            currentSpeed = 0;
            var myTransform = transform;
            var transformPosition = myTransform.position;
            transformPosition.y += 0.2f;
            transformPosition.z -= 5f;
            myTransform.position = transformPosition;
            OnCurrentPathChanged();
            yield return new WaitForSeconds(0.25f);
            _canDamage = false;
        }
    }
}
