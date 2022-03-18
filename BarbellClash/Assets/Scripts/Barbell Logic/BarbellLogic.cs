using UnityEngine;

namespace Barbell
{
    [RequireComponent(typeof(Rigidbody))]
    public class BarbellLogic : MonoBehaviour, IKinematic
    {
        private IKinematic _movementLogic;

        private void Start()
        {
            _movementLogic = new BarbellMovement(transform, transform.position.y);
        }

        public void DoMove(Vector3 direction) =>
            _movementLogic.DoMove(direction);

        public void DoUp(float upSpeed) =>
            _movementLogic.DoUp(upSpeed);

        public void DoDown(float downSpeed) =>
            _movementLogic.DoDown(downSpeed);
    }
}