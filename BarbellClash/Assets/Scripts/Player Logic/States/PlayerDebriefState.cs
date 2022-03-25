using System;
using ATG.LevelControl;
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
        private readonly ILevelStatus _lvlStatus;
        
        private const float _debriefMove = 30f;

        private Action _onGetBonus;
        private Action _moving;
        
        public PlayerDebriefState(IControllable mainObject, IStateSwitcher stateSwitcher,
            BarbellLogic bl,IBonusDetector bd, ILevelStatus lvlStatus) 
            : base(mainObject, stateSwitcher)
        {
            _bonusDetector = bd;
            _barbell = bl;

            _lvlStatus = lvlStatus;
        }

        public override void Enter()
        {
            _barbell.StopRotatePlates();
            
            Vector3 blPos = _barbell.transform.position;
            blPos.y = 8f;

            _barbell.transform.position = blPos;
            
            _onGetBonus = OnGetBonus;
            _moving = Moving;
        }

        private void Moving()
        {
            if (_bonusDetector.TargetPoint != null)
            {
                Vector3 target = _bonusDetector.TargetPoint.transform.position;
                float distance = Mathf.Abs(_barbell.transform.position.z - target.z);

                if (distance > 1f)
                {
                    Vector3 blTarget = _barbell.transform.position;
                    blTarget.z = target.z;

                    _barbell.transform.position = Vector3.MoveTowards(_barbell.transform.position, blTarget,
                        _debriefMove * Time.deltaTime);
                }
                else _onGetBonus?.Invoke();
            }
        }

        private void OnGetBonus()
        {
            _onGetBonus = null;
            _moving = null;
            
            MainObject.CinemachineService.Off();
            _bonusDetector.TargetPoint.ActivateBodybuilder(_barbell.gameObject);
            
            _lvlStatus.CompleteLevel();
        }

        public override void Execute()
        {
            _moving?.Invoke();
        }
    }
}