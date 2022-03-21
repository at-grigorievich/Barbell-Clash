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

        private Action _movement;
        
        public BarbellMovement(Transform transform, float height, ISizeable maxSize)
        {
            _transform = transform;
            _defaultHeight = height;
            
            _maxSize = maxSize;
        }


        public void DoMove(Vector3 direction)
        {
            Vector3 endDirection = _transform.position + direction + _heightDirection;

            float min = _maxSize?.Radius ?? 0f;
            endDirection.y = Mathf.Clamp(endDirection.y, min, _defaultHeight);

            _transform.position = endDirection;
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