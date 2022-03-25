using System;
using ATG.LevelControl;
using ATGStateMachine;
using Barbell;
using UnityEngine;

namespace PlayerLogic
{
    public class PlayerMoveState: BaseStatement<IControllable>
    {
        private readonly IKinematic _kinematic;
        
        public PlayerMoveState(IControllable mainObject, IStateSwitcher stateSwitcher, 
            IKinematic kinematic, ILevelStatus lvlStatus) 
            : base(mainObject, stateSwitcher)
        {
            _kinematic = kinematic;
            lvlStatus.OnDebriefStart += (sender, args) =>
                StateSwitcher.StateSwitcher<PlayerDebriefState>();
        }

        public override void Enter()
        {
            MainObject.InputService.OnStartTouch += OnStartTouch;
            MainObject.InputService.OnEndTouch += OnEndTouch;
        }

        private void OnEndTouch(object sender, EventArgs e)
        {
            _kinematic.DoUp(MainObject.SpeedParameters.DropSpeed);
        }

        private void OnStartTouch(object sender, EventArgs e)
        {
            _kinematic.DoDown(MainObject.SpeedParameters.DropSpeed);
        }

        public override void Execute()
        {
            Vector3 direction = Vector3.forward * MainObject.SpeedParameters.MovementSpeed * Time.deltaTime;
            _kinematic.DoMove(direction);
        }
    }
}