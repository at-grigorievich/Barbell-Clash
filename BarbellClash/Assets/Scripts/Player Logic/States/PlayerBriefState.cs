using System;
using ATGStateMachine;

namespace PlayerLogic
{
    public class PlayerBriefState: BaseStatement<IControllable>
    {
        public PlayerBriefState(IControllable mainObject, IStateSwitcher stateSwitcher) 
            : base(mainObject, stateSwitcher)
        {
        }

        public override void Enter()
        {
            MainObject.InputService.OnStartTouch += StartMove;
        }

        public override void Exit()
        {
            MainObject.InputService.OnStartTouch -= StartMove;
        }
        
        private void StartMove(object sender, EventArgs args) =>
            StateSwitcher.StateSwitcher<PlayerMoveState>();
    }
}