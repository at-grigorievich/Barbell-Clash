using System;
using DG.Tweening;
using PlayerLogic;
using UnityEngine;
using UnityEngine.UI;

namespace UILogic
{
    [RequireComponent(typeof(RectTransform))]
    public class SpeedProgressionVisualizer : MonoBehaviour
    {
        [SerializeField] private RectTransform _rect;
        
        [SerializeField] private Image _image;
        [SerializeField] private float _duration;
        [SerializeField] private float _scaleMin;
        [SerializeField] private float _scaleMax;

        private Tween _tween;
        
        private BoostParametersContainer _boostValues;
        
        public void Init(BoostParametersContainer bp)
        {
            _boostValues = bp;
        }

        public void UpdateValue(float curValue)
        {
            float range = _boostValues._maxSpeed - _boostValues._minSpeed;
            float cur = curValue-_boostValues._minSpeed;

            float res = Mathf.Clamp(cur / range, 0f, 1f);

            _image.fillAmount = res;

            if (Math.Abs(res - 1f) < Mathf.Epsilon)
            {
                if (_tween == null)
                {
                    _tween = _rect
                        .DOScale(_scaleMax, _duration)
                        .SetLoops(-1,LoopType.Yoyo);
                    _tween.Play();
                }
            }
            else if (_tween != null)
            {
                _tween.Kill();
                _rect.localScale = Vector3.one * _scaleMin;
                _tween = null;
            }
        }
    }
}