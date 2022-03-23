using System;
using AnimatorControl;
using Softbody.Interfaces;
using UnityEngine;

namespace Softbody
{
    [RequireComponent(typeof(IRiggable))]
    public class SoftbodyAnimator : AnimatorBehaviour, IAnimator
    {
        private IRiggable _riggable;

        private void Awake()
        {
            _riggable = GetComponent<IRiggable>();
        }

        public void Idle()
        {
          _riggable.Idle();
          SetOneState(AnimatorAction.Idle, true);
        }

        public void Crush()
        {
            _riggable.Crush();
            SetOneState(AnimatorAction.Crush, true);
        }
    }
}