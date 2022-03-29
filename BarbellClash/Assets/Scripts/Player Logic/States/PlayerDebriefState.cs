using System;
using ATGStateMachine;
using Barbell;
using Debrief;
using DG.Tweening;
using UnityEngine;

namespace PlayerLogic
{
    public class PlayerDebriefState: BaseStatement<IControllable>
    {
        public const float JumpPower = 15f;
        public const float JumpDuration = 1.5f;

        public const float MinDistance = 35f;
        
        private readonly IBonusDetector _bonusDetector;
        private readonly BarbellLogic _barbell;

        private Action _moving;

        private Vector3 _target;
        
        public PlayerDebriefState(IControllable mainObject, IStateSwitcher stateSwitcher,
            BarbellLogic bl,IBonusDetector bd) 
            : base(mainObject, stateSwitcher)
        {
            _bonusDetector = bd;
            _barbell = bl;
        }

        public override void Enter()
        {
            _barbell.StopRotatePlates();
            
            
            
            _moving = Moving;
        }
        public override void Execute()
        {
            _moving?.Invoke();
        }

        private void Moving()
        {
            if (_bonusDetector.TargetPoint != null)
            {
                _target = _bonusDetector.TargetPoint.transform.position;
                _target.y = JumpPower/2f;
                
                _moving = CheckDistance;
                _barbell.transform
                    .DOJump(_target, JumpPower, 1,JumpDuration)
                    .OnComplete(OnGetBonus);
            }
        }

        private void CheckDistance()
        {
            float distance = Mathf.Abs(MainObject.MyTransform.position.z - _target.z);
            if (distance <= MinDistance)
            {
                _moving = null;
                MainObject.CinemachineService.UpdateTarget();
                _bonusDetector.TargetPoint.Bodybuilder.EnableCinemachine();
            }
        }


        private void OnGetBonus()
        {
            _bonusDetector.TargetPoint.ActivateBodybuilder(_barbell.transform);
        }
        
    }
}