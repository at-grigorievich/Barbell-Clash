using System;
using System.Collections;
using Softbody.Interfaces;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Softbody
{
    public class BigAssRiggable : MonoBehaviour, IRiggable
    {
        [SerializeField] private ChainIKConstraint _torsoChain;
        [SerializeField] private ChainIKConstraint _leftHandChain;
        [SerializeField] private ChainIKConstraint _rightHandChain;


        public void Idle()
        {
            StartCoroutine(WaitToSet(() => _torsoChain.weight = 1f));
        }

        public void Crush()
        {
            StartCoroutine(WaitToSet(() =>
            {
                _torsoChain.weight = 0f;
                _leftHandChain.weight = 1f;
                _rightHandChain.weight = 1f;
            }));
        }

        private IEnumerator WaitToSet(Action callback)
        {
            yield return new WaitForEndOfFrame();
            callback?.Invoke();
        }
    }
}