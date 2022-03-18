using UnityEngine;

namespace Barbell
{
    public class BarbellMovement : IKinematic
    {
        private readonly Transform _transform;
        private readonly float _defaultHeight;


        private Vector3 _heightDirection;

        public BarbellMovement(Transform transform, float height)
        {
            _transform = transform;
            _defaultHeight = height;
        }


        public void DoMove(Vector3 direction)
        {
            Vector3 endDirection = _transform.position + direction + _heightDirection;
            endDirection.y = Mathf.Clamp(endDirection.y, 2f, _defaultHeight);

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