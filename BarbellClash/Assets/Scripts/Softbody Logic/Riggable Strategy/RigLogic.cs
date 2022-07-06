using System;
using System.Collections;
using Softbody.Interfaces;
using UnityEngine;

namespace Softbody
{
    public abstract class RigLogic : MonoBehaviour, IRiggable
    {
        public abstract void Idle();

        public abstract void Crush();
        public abstract void Die();

        protected IEnumerator WaitToSet(Action callback)
        {
            yield return new WaitForEndOfFrame();
            callback?.Invoke();
        }
    }
}