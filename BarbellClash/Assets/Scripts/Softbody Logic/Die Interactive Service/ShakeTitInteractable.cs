using DG.Tweening;
using UnityEngine;

namespace Softbody
{
    public class ShakeTitInteractable: DieInteractable
    {
        [SerializeField] private Transform _shakeTit;
        
        [SerializeField] private float _duration;
        [SerializeField] private Vector3 _punchForce;
        
        [ContextMenu("Test Animation")]
        public override void AnimateDie()
        {
            base.AnimateDie();
            DOTween.Sequence()
                .Append(_shakeTit.transform.DOPunchRotation(_punchForce, _duration))
                .SetLoops(2,LoopType.Incremental);
        }
    }
}