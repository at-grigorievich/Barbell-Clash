using DG.Tweening;
using UnityEngine;

namespace Softbody
{
    public class ShakeAssInteractable : DieInteractable
    {
        [SerializeField] private Transform _shakeAss;

        [SerializeField] private float _duration;
        [SerializeField] private Vector3 _punchForce;

        [Space(10)]
        [Header("Target Parameters")] 
        [SerializeField] private Vector3 _targetPosition;

        [ContextMenu("Test Animation")]
        public override void AnimateDie()
        {
            _shakeAss.transform.localPosition = _targetPosition;
            _shakeAss.transform.DOPunchRotation(_punchForce, _duration);
        }
    }
}