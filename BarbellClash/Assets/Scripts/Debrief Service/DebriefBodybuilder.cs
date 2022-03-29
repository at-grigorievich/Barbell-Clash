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
        [SerializeField] private Transform[] _toDefaultScale;
        [Header("Scale Data")]
        [SerializeField] private float _muscleScaleDuration;
        [Header("Punch Data")]
        [SerializeField] private float _musclePunchDuration;

        [SerializeField] private Vector3 _musclePunchPower;

        public void BoostMuscle(float addScale, Action callback = null)
        {
            Vector3 curScale = _muscleParent.localScale;
            Vector3 nextScale = curScale + Vector3.one * addScale;
            
            foreach (var obj in _toDefaultScale)
            {
                obj.transform.localScale = Vector3.one;
            }

            DOTween.Sequence()
                .Append(_muscleParent.DOScale(nextScale, _muscleScaleDuration))
                .Append(_muscleParent.DOPunchScale(_musclePunchPower, _musclePunchDuration, 2))
                .OnComplete(() => callback?.Invoke());
        }
    }
    public class DebriefBodybuilder : MonoBehaviour
    {
        [SerializeField] private DebriefBodybuilderAnimator _animator;
        [Space(15)]
        [SerializeField] private GameObject _cinemachineObject;
        [Space(15)] 
        [SerializeField] private Transform _confettiTarget;

        [SerializeField] private MuscleContainer _muscleContainer;
        
        [Inject] private ILevelStatus _levelStatus;
        [Inject] private IVFXControllable _vfx;
        [Inject] private IBonusDetector _bonusDetector;
        
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

            if (_bonusDetector.FinishBlockIndex > 0)
            {
                target.SetParent(HandTransform);
                target.transform.localPosition = HandPosition;
                target.transform.localRotation = Quaternion.Euler(HandRotation);

                _target = target.gameObject;
                _endBoostScale = boostScale;

                _animator.StartSquat();
            }
            else
            {
                InstantiateConfetti(true);
                _animator.AnimateWin();
                
                _levelStatus.CompleteLevel();
            }
        }

        private void OnEndSquat(object sender, EventArgs e)
        {
            Destroy(_target.gameObject);

            bool isWin = _endBoostScale > 0f;
            InstantiateConfetti(true);
            
            _muscleContainer.BoostMuscle(_endBoostScale, _levelStatus.CompleteLevel);
            _animator.AnimateWin();
        }
        

        private void InstantiateConfetti(bool isWin)
        {
            ParticleSystem ps = null;
            
            if (isWin)
            {
                ps = _vfx.PlayVFXLoop(VFXType.ConfettiShower,
                    _confettiTarget.position,
                    _confettiTarget.eulerAngles);
            }

            ps.Play();
        }
    }
}