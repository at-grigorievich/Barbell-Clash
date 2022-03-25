using System.Collections;
using UnityEngine;

namespace Debrief
{
    public class DebriefBodybuilder : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [field: SerializeField] public Transform HandTransform { get; private set; }
        [field: SerializeField] public Vector3 HandPosition { get; private set; }
        [field: SerializeField] public Vector3 HandRotation { get; private set; }
        
        public void StartSquat(int boostScale, Transform target)
        {
            _animator.SetBool("IsSquart",true);
            target.SetParent(HandTransform);
            target.transform.localPosition = HandPosition;

        }
    }
}