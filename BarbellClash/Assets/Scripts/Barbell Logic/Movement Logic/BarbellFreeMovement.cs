using UnityEngine;

namespace Barbell
{
    public class BarbellFreeMovement: IKinematic
    {
        private readonly Transform _transform;
        
        private readonly float _defaultHeight;
        private readonly float _barbellThickness;
        
        private bool _isDown = false;

        private LayerMask _mask;

        private float _heightSpeed;

        public BarbellFreeMovement(Transform transform, float height, float barbellThickness)
        {
            _transform = transform;
            
            _defaultHeight = height;
            _barbellThickness = barbellThickness;
            
            _mask = LayerMask.GetMask("Ground");
        }
        
        public void DoMove(Vector3 direction)
        {
            Vector3 rayStart = _transform.position;
            rayStart.y = _defaultHeight;

            Vector3 endDirection = _transform.position + direction;
            
            RaycastHit hit;
            if (Physics.Raycast(rayStart, -_transform.up, out hit))
            {
#if UNITY_EDITOR
                Debug.DrawRay(rayStart,-_transform.up*hit.distance);
#endif
                if (_isDown)
                {
                    endDirection.y = hit.point.y + _barbellThickness;
                }
                else
                {
                    endDirection.y = _defaultHeight;
                }
            }
            
            MoveTotal(endDirection);
        }

        public void DoUp(float upSpeed)
        {
            _isDown = false;
            _heightSpeed = upSpeed;
        }

        public void DoDown(float downSpeed)
        {
            _isDown = true;
            _heightSpeed = downSpeed;
        }

        public void UpdateCurrentHeight(ISizeable newSize){}


        public void SetUpdateMovement(bool isIgnore) {}

        private void MoveTotal(Vector3 direction)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, direction, 
              _heightSpeed * Time.deltaTime);
        }
    }
}