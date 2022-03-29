using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UILogic
{
    public class AfterGamePanel : UIPanel
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _sliderDuration;

        private Tween _sliderTween;

        private void Start()
        {
            _slider.gameObject.SetActive(false);
        }

        public override void Show()
        {
            foreach (var panelElement in elements)
            {
                panelElement.ElementEnable();
            }

            base.Show();
        }

        public override void Hide()
        {
            foreach (var panelElement in elements)
            {
                panelElement.ElementDisable();
            }

            base.Hide();
        }

        public void ShowSlider(float minValue, float maxValue)
        {
            _slider.gameObject.SetActive(true);
            _slider.minValue = minValue;
            _slider.maxValue = maxValue;
        }

        public void UpdateSliderValue(float curValue)
        {
            if (_sliderTween != null)
            {
                _sliderTween.Kill();
                _sliderTween = null;
            }

            _sliderTween = _slider.DOValue(curValue, _sliderDuration);
            _sliderTween.Play();
        }

        public void UpdateSliderValueAtMoment(float curValue)
        {
            if (_sliderTween != null)
            {
                _sliderTween.Kill();
                _sliderTween = null;
            }
            _slider.value = curValue;
        }
}
}