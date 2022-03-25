using System;
using System.Collections;
using Softbody.Interfaces;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Softbody
{
    public class BigFatRiggable : RigLogic
    {
        [SerializeField] private Rig _idleRig;
        [SerializeField] private Rig _crushRig;
        
        public override void Idle()
        {
            StartCoroutine(WaitToSet(() =>
            {
                _idleRig.weight = 1f;
                _crushRig.weight = 0f;
            }));
        }

        public override void Crush()
        {
            StartCoroutine(WaitToSet(() =>
            {
                _idleRig.weight = 0f;
                _crushRig.weight = 1f;
            }));
        }

        public override void Die() {}
    }
}