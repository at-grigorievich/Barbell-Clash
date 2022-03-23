using System.Collections;
using Barbell;
using UnityEngine;
using Zenject;
using Obi;
using Softbody.Interfaces;

namespace Softbody
{
    [RequireComponent(typeof(ObiSolver),typeof(ObiFixedUpdater))]
    public abstract class SoftbodyLogic : MonoBehaviour
    {
        [SerializeField] private PlateData _neededPlateData;
        [Space(10)]
        [SerializeField] private Vector3 _scale;

        [Inject] protected IAnimator _softbodyAnimator;

        protected ObiSolver _solver;
        protected ObiFixedUpdater _obiFixedUpdater;

        private SoftbodyMainPart _mainPart;
        
        public uint NeededPlateId => _neededPlateData.Id;
        
        private IEnumerator Start()
        {
            _solver = GetComponent<ObiSolver>();
            _obiFixedUpdater = GetComponent<ObiFixedUpdater>();
            
            yield return new WaitForEndOfFrame();
            
            transform.localScale = _scale;
            SetSoftbodyActive(false);
            
            _softbodyAnimator.Idle();
        }

        [Inject]
        private void Constructor(SoftbodyMainPart mainPart, SoftBodyCrushDetect crushDetect)
        {
            _mainPart = mainPart;

            mainPart.OnCrushContinue += (sender, args) =>
                crushDetect.ColorMainSoftbodyPart();
            mainPart.OnCrushEnd += (sender, args) =>
                _solver.parameters.damping = 1f;
        }

        public void AnimateSoftbodyCrush() => _softbodyAnimator.Crush();
        public void SetSoftbodyActive(bool isActive)
        {
            if(isActive)
                _softbodyAnimator.Crush();
            
            _solver.enabled = isActive;
            _obiFixedUpdater.enabled = isActive;
        }


        public void DoDie() => _mainPart.DisableDetecting();
        

        public class Factory: PlaceholderFactory<GameObject,SoftbodyLogic>{}
    }
}