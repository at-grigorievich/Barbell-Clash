using System;
using ATG.LevelControl;
using ATGStateMachine;

namespace PlayerLogic
{
    public class PlayerBriefState: BaseStatement<IControllable>
    {
        private readonly ILevelStatus _lvlStatus;
        
        public PlayerBriefState(IControllable mainObject, IStateSwitcher stateSwitcher, ILevelStatus lvlStatus) 
            : base(mainObject, stateSwitcher)
        {
            _lvlStatus = lvlStatus;
        }

        public override void Enter()
        {
            MainObject.InputService.OnStartTouch += StartMove;
            MainObject.InputService.OnStartTouch += StartGameplay;
        }

        public override void Exit()
        {
            MainObject.InputService.OnStartTouch -= StartMove;
            MainObject.InputService.OnStartTouch -= StartGameplay;
        }
        
        private void StartMove(object sender, EventArgs args) =>
            StateSwitcher.StateSwitcher<PlayerMoveState>();

        private void StartGameplay(object sender, EventArgs arg) => _lvlStatus.StartLevel();
    }
}