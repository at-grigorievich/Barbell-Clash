using System;
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
        private Action _updateMovement;

        private float _standartHeight;
        private float _lastHeightSpeed;
        
        public uint MaxPlateId =>
            _plateContainers[0].PlateWithMaxRadius?.Id ?? 100;

        public HeightStatus HeightStatus { get; private set; }
        
        private void Start()
        {
            foreach (var plateContainer in _plateContainers)
            {
                plateContainer.InitDefaultPlate();
            }
            _standartHeight = transform.position.y;

            _updateMovement = UpdateMovement;
        }
        private void Update()
        {
            _updateMovement?.Invoke();
        }

        public void DoMove(Vector3 direction)
        {
            _movementLogic.DoMove(direction);
        }
        public void DoUp(float upSpeed)
        {
            HeightStatus = HeightStatus.Up;
            _lastHeightSpeed = upSpeed;
            
            _movementLogic.DoUp(upSpeed);
        }
        public void DoDown(float downSpeed)
        {
            HeightStatus = HeightStatus.Down;
            _lastHeightSpeed = downSpeed;
            
            _movementLogic.DoDown(downSpeed);
        }

        public void UpdateCurrentHeight(ISizeable newHeight)
        {
            _movementLogic.UpdateCurrentHeight(newHeight);
        }

        public void SetUpdateMovement(bool isIgnore)
        {
            if (!isIgnore)
            {
                _updateMovement = UpdateMovement;
            }
            else
            {
                _updateMovement = IgnoreMovement;
            }
        }


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
            UpdateMovingHeightStatus();
        }
        private void IgnoreMovement()
        {
            ISizeable _plate = _plateContainers[0].PlateWithMaxRadius;
            if (_plate != null && !(_movementLogic is BarbellFreeMovement))
            {
                _movementLogic =new BarbellFreeMovement(transform,_standartHeight,_plate.Radius+_thickness);
                
                UpdateMovingHeightStatus();
            }
        }
        
        
        public void AddPlate(PlateLogic platePrafab)
        {
            foreach (var plateContainer in _plateContainers)
            {
                plateContainer.AddPlate(platePrafab);
            }
            
            UpdateCurrentHeight(platePrafab);
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


        private void UpdateMovingHeightStatus()
        {
            if (HeightStatus == HeightStatus.Down)
            {
                _movementLogic.DoDown(_lastHeightSpeed);
            }
            else if (HeightStatus == HeightStatus.Up)
            {
                _movementLogic.DoUp(_lastHeightSpeed);
            }
        }
        public class Factory: PlaceholderFactory<UnityEngine.Object,BarbellLogic> {}
    }
}