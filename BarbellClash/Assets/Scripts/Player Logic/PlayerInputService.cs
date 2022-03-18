using System;
using UnityEngine;

namespace PlayerLogic
{
    public class PlayerInputService : MonoBehaviour, IInputable
    {
        public event EventHandler OnStartTouch;
        public event EventHandler OnEndTouch;

        public void Update()
        {
            if (Input.touchCount > 0)
            {
                TouchPhase phase = Input.GetTouch(0).phase;

                switch (phase)
                {
                    case TouchPhase.Began:
                        OnStartTouch?.Invoke(this,EventArgs.Empty);
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        OnEndTouch?.Invoke(this,EventArgs.Empty);
                        break;
                }
            }
        }
    }
}