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
        
        private readonly IBonusDetector _bonusDetector;
        private readonly BarbellLogic _barbell;

        private Action _moving;
        
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
                _moving = null;

                Vector3 target = _bonusDetector.TargetPoint.transform.position;

                _barbell.transform
                    .DOJump(target, JumpPower, 1,JumpDuration)
                    .OnComplete(OnGetBonus);
            }
        }

        private void OnGetBonus()
        {
            _bonusDetector.TargetPoint.ActivateBodybuilder(_barbell.transform);
        }
        
    }
}