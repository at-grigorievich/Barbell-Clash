using System;
using AnimatorControl;
using PlayerLogic;
using UnityEngine;
using Zenject;

namespace Debrief
{
    public class DebriefBodybuilderAnimator : AnimatorBehaviour
    {
        [Inject] private IInputable _inputable;
        
        [SerializeField] private float _minAnimatorSpeed;
        [SerializeField] private float _maxAnimatorSpeed;
        [Space(15)]
        [SerializeField] private float _deltaAddSpeed;
        
        public void StartSquat()
        {
            SetOneState(AnimatorAction.Squat,true);

            animator.speed = _minAnimatorSpeed;
            
            _inputable.OnStartTouch += OnStartTouch;
        }

        private void OnStartTouch(object sender, EventArgs e)
        {
            float newSpeed = animator.speed + _deltaAddSpeed*Time.deltaTime;
            newSpeed = Mathf.Clamp(newSpeed, _minAnimatorSpeed, _maxAnimatorSpeed);

            animator.speed = newSpeed;
            
            Debug.Log(animator.speed);
        }
    }
}