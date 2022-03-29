using System;
using System.Collections;
using ATG.LevelControl;
using DG.Tweening;
using UnityEngine;
using VFXLogic;
using Zenject;

namespace Debrief
{
    public class DebriefBodybuilder : MonoBehaviour
    {
        [Inject] private ILevelStatus _levelStatus;
        [Inject] private IVFXControllable _vfx;
        
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _cinemachineObject;
        [SerializeField] private float _squatTime;
        [Space(15)] 
        [SerializeField] private Transform[] _confettiTargets;
         
        [field: Space(20)]        
        [field: SerializeField] public Transform HandTransform { get; private set; }
        [field: SerializeField] public Vector3 HandPosition { get; private set; }
        [field: SerializeField] public Vector3 HandRotation { get; private set; }
        
        public void StartSquat(float boostScale, Transform target)
        {
            _animator.SetBool("IsSquart",true);
            
            target.SetParent(HandTransform);
            target.transform.localPosition = HandPosition;
            target.transform.localRotation = Quaternion.Euler(HandRotation);
            
            _cinemachineObject.SetActive(true);
            
            
            InstantiateConfetti();
            SquatToScaleAnimation(boostScale);
        }

        private void SquatToScaleAnimation(float boostScale)
        {
            DOTween.Sequence()
                .Append(transform.DOScale(Vector3.one * 12, 1f))
                .OnComplete(() =>
                {
                    _levelStatus.CompleteLevel();
                });
        }

        private void InstantiateConfetti()
        {
            foreach (var confettiTarget in _confettiTargets)
            {
                ParticleSystem ps = _vfx.PlayVFX(VFXType.Confetti, confettiTarget.position, confettiTarget.eulerAngles);
                ps.Play();
            }
        }
    }
}