using System;
using System.Collections;
using Softbody.Interfaces;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Softbody
{
    public class BigFatRiggable : MonoBehaviour, IRiggable
    {
        [SerializeField] private Rig _idleRig;
        [SerializeField] private Rig _crushRig;
        
        public void Idle()
        {
            StartCoroutine(WaitToSet(() =>
            {
                _idleRig.weight = 1f;
                _crushRig.weight = 0f;
            }));
        }

        public void Crush()
        {
            StartCoroutine(WaitToSet(() =>
            {
                _idleRig.weight = 0f;
                _crushRig.weight = 1f;
            }));
        }
        
        private IEnumerator WaitToSet(Action callback)
        {
            yield return new WaitForEndOfFrame();
            callback?.Invoke();
        }
    }
}