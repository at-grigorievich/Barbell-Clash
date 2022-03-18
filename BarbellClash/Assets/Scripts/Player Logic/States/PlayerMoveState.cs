using ATGStateMachine;
using Barbell;
using UnityEngine;

namespace PlayerLogic
{
    public class PlayerMoveState: BaseStatement<IControllable>
    {
        public PlayerMoveState(IControllable mainObject, IStateSwitcher stateSwitcher) 
            : base(mainObject, stateSwitcher)
        {
        }

        public override void Enter()
        {
        }
    }
}