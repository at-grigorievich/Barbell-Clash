using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Softbody
{
    public class BigAssRiggable : RigLogic
    {
        [SerializeField] private ChainIKConstraint _torsoChain;
        [SerializeField] private ChainIKConstraint _leftHandChain;
        [SerializeField] private ChainIKConstraint _rightHandChain;


        public override void Idle()
        {
            StartCoroutine(WaitToSet(() => _torsoChain.weight = 1f));
        }

        public override void Crush()
        {
            StartCoroutine(WaitToSet(() =>
            {
                _torsoChain.weight = 0f;
                _leftHandChain.weight = 1f;
                _rightHandChain.weight = 1f;
            }));
        }

        public override void Die(){}
    }
}