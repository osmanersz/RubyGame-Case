using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class ScaleManager : MonoBehaviour
    {
        [SerializeField] private GameObject _mesh;
        [SerializeField] private float _scaleSpeed = 50f;
        private float _currentY = 1;
        private float _currentX = 1;
        private const float MinX = 1f;
        private const float MinY = 1f;
        private const float MaxX = 2f;
        private const float MaxY = 2f;
        private bool _isActive;
        private bool _scaleDirection;

        public bool CheckCurrentYScaleIsMin()
        {
            if (_currentY <= MinY)
            {
                return true;
            }
            else
                return false;
        }

        private void Awake()
        {
            var meshScale = _mesh.transform.localScale;
            _currentX = meshScale.x;
            _currentY = meshScale.y;
        }

        public void SizeUpdate(float inputY)
        {
            if (inputY < 0)
            {
                _scaleDirection = true;
            }
            else
            {
                _scaleDirection = false;
            }

            if (!_isActive)
            {
                _isActive = true;
                StartCoroutine(SizeUpdateIe());
            }
        }

        public void SizeUpdateEnd()
        {
            _isActive = false;
        }

        private IEnumerator SizeUpdateIe()
        {
            while (_isActive)
            {
                yield return new WaitForFixedUpdate();
                MeshSizeControl(_scaleDirection);
            }
        }


        private void MeshSizeControl(bool direction)
        {
            float value = 0.025f;
            if (direction)
            {
                if (_currentX < MaxX)
                {
                    _currentX += value;
                    if (_currentY > MinY)
                    {
                        _currentY -= value;
                    }
                }
            }
            else
            {
                if (_currentY < MaxY)
                {
                    _currentY += value;
                    if (_currentX > MinX)
                    {
                        _currentX -= value;
                    }
                }
            }

            var newScale = new Vector3(_currentX, _currentY, 1);
            _mesh.transform.localScale = Vector3.Lerp(_mesh.transform.localScale, newScale,
                _scaleSpeed * Time.fixedDeltaTime);
        }
    }
}
