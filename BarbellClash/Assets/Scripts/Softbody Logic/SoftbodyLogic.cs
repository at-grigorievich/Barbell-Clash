using System.Collections;
using Barbell;
using UnityEngine;
using Zenject;
using Obi;

namespace Softbody
{
    [RequireComponent(typeof(ObiSolver),typeof(ObiFixedUpdater))]
    public abstract class SoftbodyLogic : MonoBehaviour
    {
        [SerializeField] private SoftbodyMainPart _mainPart;
        
        [SerializeField] private Vector3 _scale;
        
        protected ObiSolver _solver;
        protected ObiFixedUpdater _obiFixedUpdater;

        protected IKinematic _curKinematic;

        private IEnumerator Start()
        {
            _solver = GetComponent<ObiSolver>();
            _obiFixedUpdater = GetComponent<ObiFixedUpdater>();

            _mainPart.OnCrushEnd +=
                (sender, args) => _solver.parameters.damping = 1f;
            
            yield return new WaitForEndOfFrame();
            transform.localScale = _scale;
            SetSoftbodyActive(false);
        }
        

        public void SetSoftbodyActive(bool isActive)
        {
            _solver.enabled = isActive;
            _obiFixedUpdater.enabled = isActive;
        }
        public class Factory: PlaceholderFactory<GameObject,SoftbodyLogic>{}
    }
}