using System;
using ATG.LevelControl;
using Barbell;
using PlayerLogic;
using UnityEngine;
using VFXLogic;
using Zenject;

namespace Obstacles
{
    public class ObstacleBlock: EnvironmentBlock
    {
        [SerializeField] private ObstacleElement[] _obstacles;
        [SerializeField] private Transform _hitEffectTarget;
        
        [Inject] private IVFXControllable _vfxControllable;
        [Inject] private IBoostable _boostable;

        private Action<BarbellLogic> _offObstacles;
        
        private void Awake()
        {
            _offObstacles = OffObstacles;
        }

        private void OffObstacles(BarbellLogic bl)
        {
            _offObstacles = null;

            if (bl.HeightStatus == HeightStatus.Down)
            {
                _boostable.RemoveBoostSpeed();

                var vfx = _vfxControllable.PlayVFX(VFXType.Death,
                    _hitEffectTarget.position, 
                    _hitEffectTarget.eulerAngles);
                foreach (var obstacleElement in _obstacles)
                {
                    obstacleElement.OffObstacle();
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if(other.attachedRigidbody == null)
                return;
            
            if(other.TryGetComponent(out IgnoreTriggers it))
                return;

            if (other.attachedRigidbody.TryGetComponent(out ICrushable crush))
            {
                if (crush is BarbellLogic bl)
                {
                    _offObstacles?.Invoke(bl);
                }
            }
        }
    }
}