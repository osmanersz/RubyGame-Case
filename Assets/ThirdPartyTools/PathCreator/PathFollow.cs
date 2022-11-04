using System;
using PathCreation;
using UnityEngine;

namespace PathCreator
{
    public abstract class PathFollow : MonoBehaviour
    {
        [SerializeField] private PathCreation.PathCreator currentPathCreator;
        [SerializeField] private EndOfPathInstruction endOfPathInstruction;
        [Header("")]  
        [SerializeField] protected float currentSpeed = 1;
        [Range(1, 20)] [SerializeField] private float angleTurnSpeed = 1f;
        protected bool _isActive = false;
        private float _distanceTravelled;
        private Rigidbody _myRigidbody;

        private void Awake()
        {
            _myRigidbody = GetComponent<Rigidbody>();
        }

        public void Activate()
        {
            if (currentPathCreator!=null)
            {
                _myRigidbody.isKinematic = false;
                Set();
            }
        }

        public void Close()
        {
            _isActive = false;
            Invoke(nameof(IsKinematicClose),1f);
        }

        private void IsKinematicClose()
        {
            _myRigidbody.isKinematic = true;
        }

        private void Set()
        {
            OnCurrentPathChanged();
            _myRigidbody.MoveRotation(GetRotation());
            _isActive = true;
        }

        protected virtual void FixedUpdate()
        {
            if (!(currentPathCreator != null & _isActive))
            {
                return;
            }

            _distanceTravelled += currentSpeed * Time.fixedDeltaTime;
            var newPosition = currentPathCreator.path.GetPointAtDistance(_distanceTravelled, endOfPathInstruction);
            var myPosition = transform.position;

            newPosition.y = myPosition.y;
            _myRigidbody.MovePosition(newPosition);

            Quaternion deltaRotation = Quaternion.Slerp(_myRigidbody.rotation, GetRotation(),
                angleTurnSpeed * Time.fixedDeltaTime);
            _myRigidbody.MoveRotation(deltaRotation);
        }

        private Quaternion GetRotation()
        {
            var rotation =
                currentPathCreator.path.GetRotationAtDistance(_distanceTravelled, endOfPathInstruction);
            if (Math.Abs(rotation.x - 1) < 0.1f)
            {
                var rot = rotation;
                rot.x = 0;
                rot.y = rotation.x;
                rotation = rot;
            }

            return rotation;
        }

        protected void OnCurrentPathChanged()
        {
            _distanceTravelled = currentPathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}
