using System;
using UnityEngine;

namespace Barbell
{
    public class BarbellMovement : IKinematic
    {
        private readonly Transform _transform;
        private ISizeable _maxSize;
        private Vector3 _heightDirection;
        private  float _defaultHeight;

        
        private readonly Action _startPlateRotate, _stopPlateRotate;
        
        public BarbellMovement(Transform transform, float height, 
            ISizeable maxSize, Action startPlateRotate,Action stopPlateRotate)
        {
            _transform = transform;
            _defaultHeight = height;
            
            _maxSize = maxSize;

            _startPlateRotate = startPlateRotate;
            _stopPlateRotate = stopPlateRotate;
        }


        public void DoMove(Vector3 direction)
        {
            Vector3 endDirection = _transform.position + direction + _heightDirection;

            float min = _maxSize?.Radius ?? 0f;
            endDirection.y = Mathf.Clamp(endDirection.y, min, _defaultHeight);

            _transform.position = endDirection;
            
            if (Mathf.Abs(_transform.position.y - _defaultHeight) > 0.1f)
            {
                _startPlateRotate?.Invoke();
            }
            else
            {
                _stopPlateRotate?.Invoke();
            }
        }

        public void DoUp(float upSpeed)
        {
            _heightDirection = Vector3.up * upSpeed * Time.deltaTime;
        }

        public void DoDown(float downSpeed)
        {
            _heightDirection = Vector3.down * downSpeed * Time.deltaTime;
        }

        public void UpdateCurrentHeight(ISizeable newSize)
        {
            _maxSize = newSize;
        }

        public void UpdateCurrentHeight(float newHeight)
        {
            _defaultHeight = newHeight;
        }

        public void SetUpdateMovement(bool isIgnore) {}
    }
}