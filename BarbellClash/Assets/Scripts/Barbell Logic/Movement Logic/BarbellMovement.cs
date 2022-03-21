using System;
using UnityEngine;

namespace Barbell
{
    public class BarbellMovement : IKinematic
    {
        private readonly Transform _transform;
        private readonly float _defaultHeight;

        private readonly ISizeable _maxSize;

        private Vector3 _heightDirection;

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

            if (Mathf.Abs(_transform.position.y - min) <= 0.1f)
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
    }
}