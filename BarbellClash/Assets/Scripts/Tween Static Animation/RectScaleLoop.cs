using System;
using DG.Tweening;
using UnityEngine;

namespace TweenStatic
{
    [RequireComponent(typeof(RectTransform))]
    public class RectScaleLoop : MonoBehaviour
    {
        [SerializeField] private MaxMinContainer data;
        [SerializeField] private float duration;
        [SerializeField] private float shakeStrength = 0.15f;
        
        private RectTransform rect;

        private Sequence _sequence;
        
        private void Awake()
        {
            rect = GetComponent<RectTransform>();

            _sequence = DOTween.Sequence()
                .Append(rect.DOScale(data.max, duration))
                .SetLoops(-1, LoopType.Yoyo);

            _sequence.Play();
        }

        public void ShakeScale(Action callback)
        {
            if(_sequence != null)
                _sequence.Kill();

            rect
                .DOPunchScale(Vector3.one*shakeStrength,Mathf.Sqrt(duration)/4f,5,0.4f )
                .OnComplete(() => callback?.Invoke());
        }
    }
}
