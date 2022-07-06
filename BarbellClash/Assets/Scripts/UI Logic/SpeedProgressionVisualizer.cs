using System;
using ATG.LevelControl;
using Debrief;
using DG.Tweening;
using PlayerLogic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UILogic
{
    [RequireComponent(typeof(RectTransform))]
    public class SpeedProgressionVisualizer : MonoBehaviour
    {
        [Inject] private ILevelStatus _levelStatus;
        [Inject] private IBonusDetector _bonusDetector;
        
        [SerializeField] private RectTransform _rect;
        [SerializeField] private float _lerpSpeed;
        
        [SerializeField] private Image _image;
        [SerializeField] private float _duration;
        [SerializeField] private float _scaleMin;
        [SerializeField] private float _scaleMax;

        [Space(15)] 
        [SerializeField] private Color _scaleColor;
        
        private PlayerLogicService _playerLogicService;

        private float _maxValue, _minValue;
        private float _totalValue;

        private float _nextValue;

        private Tween _tween;
        private Sequence _fillTween;

        private Color _defaultColor;
        
        public float CurrentPercentProgress => _image.fillAmount;

        private Action _fillCallback;
        
        private void Awake()
        {
            _defaultColor = _image.color;
            
            var player = FindObjectOfType<PlayerLogicService>();
            if (player != null)
            {
                _playerLogicService = player;

                _maxValue = _playerLogicService.BoostData._maxSpeed;
                _minValue = _playerLogicService.BoostData._minSpeed;
            }
            else throw new NullReferenceException("Cant Find Player on scene!!");
        }

        private void Start()
        {
            _fillCallback = FillCallback;

            _levelStatus.OnDebriefStart += (sender, args) =>
            {
                _fillCallback = null;
                _totalValue = _image.fillAmount;
            };
        }
        private void Update()
        {
            _fillCallback?.Invoke();
        }

        public void RemoveFillPercent()
        {
            float delta = _totalValue/_bonusDetector.FinishBlockIndex;
            if (float.IsNaN(delta))
            {
                gameObject.transform.parent.gameObject.SetActive(false);
            }

            float resFill = _image.fillAmount - delta;

            if (_fillTween != null)
            {
                _image.fillAmount = _nextValue;
                _image.color = _defaultColor;
                _fillTween.Kill();
                _fillTween = null;
            }

            _nextValue = resFill;
            
            _tween.Kill();
            _fillTween = DOTween.Sequence();
            _fillTween
                .Append(_image.DOFillAmount(resFill, 0.15f))
                .Join(_rect.DOPunchScale(Vector3.one * 0.1f, 0.15f,1))
                .Join(_image.DOColor(_scaleColor,0.1f))
                .OnComplete(() => _image.color = _defaultColor);
            
            _fillTween.Play();
        }
        
        private void FillCallback()
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