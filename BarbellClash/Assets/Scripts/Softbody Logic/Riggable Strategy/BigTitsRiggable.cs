using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Softbody
{
    public class BigTitsRiggable: RigLogic
    {
        [SerializeField] private float _speed;
        [Space(10)]
        [SerializeField] private Rig _idleRig;
        [SerializeField] private Rig _crushRig;
        [SerializeField] private Rig _dieRig;
        
        public override void Idle()
        {
            StartCoroutine(WaitToSet(() =>
            {
                _dieRig.weight = 0f;
                _crushRig.weight = 0f;
                _idleRig.weight = 1f;
            }));
        }

        public override void Crush()
        {
            StartCoroutine(WaitToSet(() =>
            {
                _idleRig.weight = 0f;
                _dieRig.weight = 0f;
                _crushRig.weight = 1f;
            }));
        }

        public override void Die()
        {
            StartCoroutine(WaitToSet(() =>
            {
                _idleRig.weight = 0f;
                _dieRig.weight = 0f;
            }));
            StartCoroutine(AnimateDie(false));
        }

        private IEnumerator AnimateDie(bool isInvert)
        {
            float curTime = 0f;

            float needValue = isInvert ? 0f : 1f;
            while (Mathf.Abs(_dieRig.weight - needValue) > Mathf.Epsilon)
            {
                yield return new WaitForEndOfFrame();

                _dieRig.weight = Mathf.MoveTowards(_dieRig.weight,needValue,_speed*Time.deltaTime);
                curTime += Time.deltaTime;
            }
        }
    }
}