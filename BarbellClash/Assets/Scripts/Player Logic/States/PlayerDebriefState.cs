using System;
using ATGStateMachine;
using Barbell;
using Debrief;
using UnityEngine;

namespace PlayerLogic
{
    public class PlayerDebriefState: BaseStatement<IControllable>
    {
        private readonly IBonusDetector _bonusDetector;
        private readonly BarbellLogic _barbell;

        private const float _debriefMove = 30f;

        private Action _onGetBonus;
        
        public PlayerDebriefState(IControllable mainObject, IStateSwitcher stateSwitcher,
            BarbellLogic bl,IBonusDetector bd) 
            : base(mainObject, stateSwitcher)
        {
            _bonusDetector = bd;
            _barbell = bl;
        }

        public override void Enter()
        {
            Vector3 blPos = _barbell.transform.position;
            blPos.y = 8f;

            _barbell.transform.position = blPos;
            
            _onGetBonus = OnGetBonus;
        }

        private void OnGetBonus()
        {
            _onGetBonus = null;
        }

        public override void Execute()
        {
            if (_bonusDetector.TargetPoint != null)
            {
                Vector3 target = (Vector3) _bonusDetector.TargetPoint;
                float distance = Mathf.Abs(_barbell.transform.position.z - target.z);

                if (distance > Mathf.Epsilon)
                {
                    Vector3 blTarget = _barbell.transform.position;
                    blTarget.z = target.z;

                    _barbell.transform.position = Vector3.MoveTowards(_barbell.transform.position, blTarget,
                        _debriefMove * Time.deltaTime);
                }
                else _onGetBonus?.Invoke();
            }
        }
    }
}