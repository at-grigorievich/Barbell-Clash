using System;
using Barbell;
using UnityEngine;

namespace Softbody
{
    public class SoftbodyMainPart : MonoBehaviour
    {
        public event EventHandler OnCrushStart;
        public event EventHandler OnCrushEnd;
        
        private IKinematic _kinematic;

        private void OnTriggerEnter(Collider other)
        {
            if(_kinematic != null || other.attachedRigidbody == null)
                return;

            if (other.attachedRigidbody.TryGetComponent(out IKinematic kinematic))
            {
                _kinematic = kinematic;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if(_kinematic == null || other.attachedRigidbody == null)
                return;

            if (other.attachedRigidbody.TryGetComponent(out IKinematic kinematic))
            {
                if (ReferenceEquals(kinematic, _kinematic))
                {
                    _kinematic = null;
                    OnCrushEnd?.Invoke(this,EventArgs.Empty);
                }
            }
        }
    }
}