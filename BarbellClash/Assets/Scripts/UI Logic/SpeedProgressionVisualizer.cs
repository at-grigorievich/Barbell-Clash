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
        [SerializeField] private float _lerpSpeed;
        
        [SerializeField] private Image _image;
        [SerializeField] private float _duration;
        [SerializeField] private float _scaleMin;
        [SerializeField] private float _scaleMax;

        private PlayerLogicService _playerLogicService;

        private float _maxValue, _minValue;
        
        private Tween _tween;
        
        private void Awake()
        {
            var player = FindObjectOfType<PlayerLogicService>();
            if (player != null)
            {
                _playerLogicService = player;

                _maxValue = _playerLogicService.BoostData._maxSpeed;
                _minValue = _playerLogicService.BoostData._minSpeed;
            }
            else throw new NullReferenceException("Cant Find Player on scene!!");
        }

        private void Update()
        {
            float range = _maxValue - _minValue;
            float current = _playerLogicService.ProgressValue - _minValue;

            float res = Mathf.Clamp(current / range, 0f, 1f);
            
            _image.fillAmount = Mathf.Lerp(_image.fillAmount, res, Time.deltaTime * _lerpSpeed);
            
            CheckFill(res);
        }

        private void CheckFill(float res)
        {
            if (Math.Abs(res - 1f) <= Mathf.Epsilon)
            {
                if (_tween == null)
                {
                    _tween = _rect
                        .DOScale(_scaleMax, _duration)
                        .SetLoops(-1,LoopType.Yoyo);
                    _tween.Play();
                }
            }
            else if(_tween != null)
            {
                _tween.Kill();
                _rect.localScale = Vector3.one * _scaleMin;
                _tween = null;
            }
        }
    }
}