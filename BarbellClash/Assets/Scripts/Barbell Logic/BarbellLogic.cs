using UnityEngine;
using Zenject;

namespace Barbell
{
    [RequireComponent(typeof(Rigidbody))]
    public class BarbellLogic : MonoBehaviour, IKinematic, ICrushable, IStackable
    {
        [Range(0f,10f)]
        [SerializeField] private float _thickness;

        [SerializeField] private Collider _crushCollider;
        
        [Inject] private IPlateContainer[] _plateContainers;
        
        private IKinematic _movementLogic;

        private float _standartHeight;
        
        public uint MaxPlateId =>
            _plateContainers[0].PlateWithMaxRadius?.Id ?? 100;
        
        private void Start()
        {
            foreach (var plateContainer in _plateContainers)
            {
                plateContainer.InitDefaultPlate();
            }
            _standartHeight = transform.position.y;
        }
        private void Update()
        {
            UpdateMovement();
        }

        public void DoMove(Vector3 direction) =>
            _movementLogic.DoMove(direction);
        public void DoUp(float upSpeed) =>
            _movementLogic.DoUp(upSpeed);
        public void DoDown(float downSpeed) =>
            _movementLogic.DoDown(downSpeed);
        
        
        private void UpdateMovement()
        {
            ISizeable _plate = _plateContainers[0].PlateWithMaxRadius;
            if (_plate != null)
            {
                if (!(_movementLogic is BarbellMovement))
                {
                    _movementLogic = new BarbellMovement(transform, 
                        _standartHeight,
                        _plateContainers[0].PlateWithMaxRadius,
                        StartAnimatePlatesRotate,
                        EndAnimatePlatesRotate);
                }
            }
            else
            {
                if (!(_movementLogic is BarbellFreeMovement))
                {
                    _movementLogic = new BarbellFreeMovement(transform,_standartHeight,_thickness);
                }
            }
        }
        
        public void AddPlate(PlateLogic platePrafab)
        {
            foreach (var plateContainer in _plateContainers)
            {
                plateContainer.AddPlate(platePrafab);
            }
        }
        private void StartAnimatePlatesRotate()
        {
            foreach (var plateContainer in _plateContainers)
            {
                plateContainer.StartRotatePlates();
            }
        }
        private void EndAnimatePlatesRotate()
        {
            foreach (var plateContainer in _plateContainers)
            {
                plateContainer.StopRotatePlates();
            }
        }
        
        public void SetCrushCollider(bool enabled)
        {
            _crushCollider.enabled = enabled;
        }
        
        public class Factory: PlaceholderFactory<UnityEngine.Object,BarbellLogic> {}
    }
}