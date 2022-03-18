using UnityEngine;
using Zenject;

namespace Barbell
{
    [RequireComponent(typeof(Rigidbody))]
    public class BarbellLogic : MonoBehaviour, IKinematic
    {
        [Inject] private IPlateContainer[] _plateContainers;
        
        private IKinematic _movementLogic;

        private void Start()
        {
            foreach (var plateContainer in _plateContainers)
            {
                plateContainer.InitDefaultPlate();
            }
            _movementLogic = 
                new BarbellMovement(transform, 
                    transform.position.y,
                    _plateContainers[0].PlateWithMaxRadius);
        }

        public void DoMove(Vector3 direction) =>
            _movementLogic.DoMove(direction);

        public void DoUp(float upSpeed) =>
            _movementLogic.DoUp(upSpeed);

        public void DoDown(float downSpeed) =>
            _movementLogic.DoDown(downSpeed);
        
        public class Factory: PlaceholderFactory<UnityEngine.Object,BarbellLogic> {}
    }
}