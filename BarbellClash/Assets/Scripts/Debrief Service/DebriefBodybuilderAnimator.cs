using System;
using AnimatorControl;
using PlayerLogic;
using UILogic;
using UnityEngine;
using Zenject;

namespace Debrief
{
    public class DebriefBodybuilderAnimator : AnimatorBehaviour
    {
        [Inject] private IInputable _inputable;
        [Inject] private UIControlSystem _uiControl;
        
        [SerializeField] private float _minAnimatorSpeed;
        [SerializeField] private float _maxAnimatorSpeed;
        [Space(15)]
        [SerializeField] private float _deltaAddSpeed;
        [SerializeField] private float _deltaRemoveSpeed;

        private AfterGamePanel _afterGameUI;

        private Action _decreaseAnimatorSpeed;
        
        private void Start()
        {
            _afterGameUI = _uiControl.GetPanel<AfterGamePanel>();
        }

        private void Update()
        {
            _decreaseAnimatorSpeed?.Invoke();
        }

        public void StartSquat()
        {
            SetOneState(AnimatorAction.Squat,true);

            animator.speed = _minAnimatorSpeed;
            
            _inputable.OnStartTouch += OnStartTouch;
            _inputable.OnEndTouch += OnEndTouch;

            _afterGameUI.ShowSlider(_minAnimatorSpeed,_maxAnimatorSpeed);
            _afterGameUI.UpdateSliderValueAtMoment(_minAnimatorSpeed);
        }
        
        private void DecreaseAnimatorSpeed()
        {
            if (animator.speed > _minAnimatorSpeed && animator.speed < _maxAnimatorSpeed)
            {
                UpdateAnimatorSpeed(false);
                _afterGameUI.UpdateSliderValueAtMoment(animator.speed);
            }
        }

        private void OnStartTouch(object sender, EventArgs e)
        {
            _decreaseAnimatorSpeed = null;
            
            UpdateAnimatorSpeed(true);
            _afterGameUI.UpdateSliderValue(animator.speed);
        }
        private void OnEndTouch(object sender, EventArgs e)
        {
            _decreaseAnimatorSpeed = DecreaseAnimatorSpeed;
        }
        

        private void UpdateAnimatorSpeed(bool isAdd)
        {
            float newSpeed = 0f;

            if (isAdd)
            {
                newSpeed = animator.speed + _deltaAddSpeed * Time.deltaTime;
            }
            else
            {
                newSpeed = animator.speed - _deltaRemoveSpeed * Time.deltaTime;
            }
            newSpeed = Mathf.Clamp(newSpeed, _minAnimatorSpeed, _maxAnimatorSpeed);

            animator.speed = newSpeed;
        }
    }
}