using System;
using ATG.LevelControl;
using DG.Tweening;
using UnityEngine;
using VFXLogic;
using Zenject;

namespace Debrief
{
    [Serializable]
    public class MuscleContainer
    {
        [SerializeField] private Transform _muscleParent;
        [Header("Scale Data")]
        [SerializeField] private float _muscleScaleDuration;
        [Header("Punch Data")]
        [SerializeField] private float _musclePunchDuration;

        [SerializeField] private Vector3 _musclePunchPower;

        public void BoostMuscle(float addScale)
        {
            Vector3 curScale = _muscleParent.localScale;
            Vector3 nextScale = curScale + Vector3.one * addScale;

            DOTween.Sequence()
                .Append(_muscleParent.DOScale(nextScale, _muscleScaleDuration))
                .Append(_muscleParent.DOPunchScale(_musclePunchPower, _musclePunchDuration,2,1f));
        }
    }
    public class DebriefBodybuilder : MonoBehaviour
    {
        [SerializeField] private DebriefBodybuilderAnimator _animator;
        [Space(15)]
        [SerializeField] private GameObject _cinemachineObject;
        [Space(15)] 
        [SerializeField] private Transform[] _confettiTargets;

        [SerializeField] private MuscleContainer _muscleContainer;
        
        [Inject] private ILevelStatus _levelStatus;
        [Inject] private IVFXControllable _vfx;

        [field: Space(20)]        
        [field: SerializeField] public Transform HandTransform { get; private set; }
        [field: SerializeField] public Vector3 HandPosition { get; private set; }
        [field: SerializeField] public Vector3 HandRotation { get; private set; }

        private GameObject _target;
        private float _endBoostScale;
        
        public void EnableCinemachine() => _cinemachineObject.SetActive(true);
        
        public void StartSquat(float boostScale, Transform target)
        {
            _animator.OnEndSquat += OnEndSquat;
            
            target.SetParent(HandTransform);
            target.transform.localPosition = HandPosition;
            target.transform.localRotation = Quaternion.Euler(HandRotation);

            _target = target.gameObject;
            _endBoostScale = boostScale;
            
            _animator.StartSquat();
        }

        private void OnEndSquat(object sender, EventArgs e)
        {
            Destroy(_target.gameObject);
            
            InstantiateConfetti();
            
            _muscleContainer.BoostMuscle(_endBoostScale);
            _animator.AnimateWin();
        }


        private void SquatToScaleAnimation(float boostScale)
        {
            DOTween.Sequence()
                .Append(transform.DOScale(Vector3.one * 12, 1f))
                .OnComplete(() =>
                {
                    //_levelStatus.CompleteLevel();
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