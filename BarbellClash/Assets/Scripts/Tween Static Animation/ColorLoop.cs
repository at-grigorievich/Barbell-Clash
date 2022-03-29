using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TweenStatic
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ColorLoop : MonoBehaviour
    {
        [SerializeField] private Color _firstColor;
        [SerializeField] private Color _secondColor;
        [Space(10)] 
        [SerializeField] private float _duration;

        private TextMeshProUGUI _text;
        
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _text.color = _firstColor;

            DOTween.Sequence()
                .Append(_text.DOColor(_firstColor, _duration))
                .Append(_text.DOColor(_secondColor, _duration))
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}