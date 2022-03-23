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

        [Inject] private IAnimator _softbodyAnimator;
        
        protected ObiSolver _solver;
        protected ObiFixedUpdater _obiFixedUpdater;

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
            mainPart.OnCrushContinue += (sender, args) =>
                crushDetect.ColorMainSoftbodyPart();
            mainPart.OnCrushEnd += (sender, args) =>
                _solver.parameters.damping = 1f;
        }

        public void SetSoftbodyActive(bool isActive)
        {
            if(isActive)
                _softbodyAnimator.Crush();
            
            _solver.enabled = isActive;
            _obiFixedUpdater.enabled = isActive;
        }
        public class Factory: PlaceholderFactory<GameObject,SoftbodyLogic>{}
    }
}