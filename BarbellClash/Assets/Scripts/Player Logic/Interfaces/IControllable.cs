using UnityEngine;

namespace PlayerLogic
{
    public interface IControllable
    {
        SpeedValues SpeedParameters { get; }
        
        Transform MyTransform { get; }
        
        ICinemachinable CinemachineService { get; }
        IInputable InputService { get; }
    }
}