using System;
using Barbell;
using UnityEngine;

namespace Softbody
{
    public class SoftbodyMainPart : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        
        public event EventHandler OnCrushStart;
        public event EventHandler OnCrushContinue;
        public event EventHandler OnCrushEnd;
        
        
        private ICrushable _kinematic;

        public void DisableDetecting()
        {
            _collider.enabled = false;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if(_kinematic != null || other.attachedRigidbody == null)
                return;
            
            if (other.attachedRigidbody.TryGetComponent(out ICrushable kinematic))
            {
                _kinematic = kinematic;
                
                kinematic.SetCrushCollider(true);
                OnCrushStart?.Invoke(this,EventArgs.Empty);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (_kinematic == null || other.attachedRigidbody == null)
                return;

            if (other.attachedRigidbody.TryGetComponent(out ICrushable kinematic))
            {
                if (ReferenceEquals(kinematic, _kinematic))
                {
                    OnCrushContinue?.Invoke(this,EventArgs.Empty);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(_kinematic == null || other.attachedRigidbody == null)
                return;

            if (other.attachedRigidbody.TryGetComponent(out ICrushable kinematic))
            {
                if (ReferenceEquals(kinematic, _kinematic))
                {
                    _kinematic.SetCrushCollider(false);
                    
                    _kinematic = null;
                    OnCrushEnd?.Invoke(this,EventArgs.Empty);
                }
            }
        }
    }
}