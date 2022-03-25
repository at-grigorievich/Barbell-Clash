using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace TweenStatic
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class RectTextShowForTime : MonoBehaviour
    {
        [SerializeField] private bool isHiddenAwake;
        [Space(5)] 
        [SerializeField] private float delay;
        [SerializeField] private float duration;
        
        private TextMeshProUGUI _textMesh;
        
        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
            
            if (isHiddenAwake)
            {
                Color curColor = _textMesh.color;
                curColor.a = 0;

                _textMesh.color = curColor;
            }
        }

        public void ShowOnTextData(string data)
        {
            _textMesh.SetText(data);

            Color firstColor = _textMesh.color;
            Color secondColor = _textMesh.color;
            
            firstColor.a = 1f;
            secondColor.a = 0f;

            DOTween.Sequence()
                .Append(_textMesh.DOColor(firstColor, duration))
                .AppendInterval(delay)
                .Append(_textMesh.DOColor(secondColor, duration));
        }
        public void ShowOffTextData(string data)
        {
            _textMesh.SetText(data);

            Color firstColor = _textMesh.color;
            Color secondColor = _textMesh.color;
            
            firstColor.a = 1f;
            secondColor.a = 0f;

            DOTween.Sequence()
                .Append(_textMesh.DOColor(secondColor, duration))
                .AppendInterval(delay)
                .Append(_textMesh.DOColor(firstColor, duration));
        }
        
    }
}