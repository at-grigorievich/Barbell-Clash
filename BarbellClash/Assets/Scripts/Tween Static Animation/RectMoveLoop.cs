using System;
using DG.Tweening;
using UnityEngine;

namespace TweenStatic
{
    [RequireComponent(typeof(RectTransform))]
    public class RectMoveLoop : MonoBehaviour
    {
        public enum RectMoveType
        {
            Punch,
            Linear
        }
        
        [SerializeField] private Vector3 punchVector;
        [SerializeField] private RectMoveType moveType;
        [SerializeField] private float duration;
        [SerializeField] private float shakeStrength = 0.15f;
        
        private RectTransform rect;

        private Sequence _sequence;
        
        private void Awake()
        {
            rect = GetComponent<RectTransform>();


            if (moveType == RectMoveType.Punch)
            {
               _sequence = DOTween.Sequence()
                    .Append(rect.DOPunchPosition(punchVector, duration, 2, 1f))
                    .SetLoops(-1, LoopType.Yoyo);
            }
            else if (moveType == RectMoveType.Linear)
            {
                _sequence = DOTween.Sequence()
                    .Append(rect.DOAnchorPos(punchVector,duration))
                    .SetLoops(-1, LoopType.Yoyo);   
            }

            _sequence.Play();
        }
    }
}