using System;
using Barbell;
using PlayerLogic;
using Softbody.Interfaces;
using UnityEngine;
using Zenject;

namespace Softbody
{
    public enum CrushType
    {
        Crush,
        Ignore
    }
    
    public class SoftbodyMainPart : MonoBehaviour
    {
        [SerializeField] private Collider _collider;

        [Inject(Id = "NeedPlate")] private uint _neededPlateId;
        
        [Inject] private IDieInteractable _dieInteractable;
        [Inject] private IAnimator _animator;
        [Inject] private IVisualable _visualable;
        [Inject] private IBoostable _boostable;

        public event EventHandler OnCrushStart;
        public event EventHandler OnCrushContinue;
        public event EventHandler OnCrushEnd;

        private CrushType _crushType = CrushType.Ignore;
        
        private ICrushable _kinematic;

        private Action _addBoostSpeed;

        private void Awake()
        {
            _addBoostSpeed = AddBoostSpeed;
        }

        public void DisableDetecting()
        {
            _collider.enabled = false;
        }
        public void ChangeBarbellIgnoreMovement(ICrushable k, bool isIgnore)
        {
            if (k is IKinematic kinematic)
            {
                kinematic.SetUpdateMovement(isIgnore);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if(_kinematic != null || other.attachedRigidbody == null)
                return;
            
            if (other.attachedRigidbody.TryGetComponent(out ICrushable crush))
            {
                _kinematic = crush;
                
                if (_neededPlateId == crush.MaxPlateId)
                {
                    DoCrushSoftbody(crush);
                }
                else if (_neededPlateId > crush.MaxPlateId)
                {
                    DoDieSoftbody(crush,other.attachedRigidbody.transform.position);
                }
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
                    if (_crushType == CrushType.Crush)
                    {
                        _addBoostSpeed?.Invoke();
                        OnCrushContinue?.Invoke(this,EventArgs.Empty);
                    }
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if(_kinematic == null || other.attachedRigidbody == null)
                return;

            if (other.attachedRigidbody.TryGetComponent(out ICrushable crushable))
            {
                if (ReferenceEquals(crushable, _kinematic))
                {
                    _kinematic.SetCrushCollider(false);
                    DisableDetecting();
                    
                    _kinematic = null;
                    _crushType = CrushType.Ignore;
                    
                    OnCrushEnd?.Invoke(this,EventArgs.Empty);
                }
            }
        }

        private void DoCrushSoftbody(ICrushable crushable)
        {
            _visualable.SmokeAfterShake();
            _visualable.ShowCompleteEmotion();
                
            crushable.SetCrushCollider(true);
            OnCrushStart?.Invoke(this,EventArgs.Empty);

            _crushType = CrushType.Crush;
        }
        private void DoDieSoftbody(ICrushable crushable, Vector3 position)
        {
            _dieInteractable.AnimateDie();
            _animator.Die();
            
            _visualable.ShowDieEmotion();
            _visualable.ShowHitDie(position);
            
            _boostable.RemoveBoostSpeed();
            
            ChangeBarbellIgnoreMovement(crushable,true);
        }
        
        private void AddBoostSpeed()
        {
            _boostable.AddBoostSpeed();
            _addBoostSpeed = null;
        }
        
    }
}