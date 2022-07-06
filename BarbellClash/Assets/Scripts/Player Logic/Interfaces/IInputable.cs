using System;

namespace PlayerLogic
{
    public interface IInputable
    {
        event EventHandler OnStartTouch;
        event EventHandler OnEndTouch;
    }
}