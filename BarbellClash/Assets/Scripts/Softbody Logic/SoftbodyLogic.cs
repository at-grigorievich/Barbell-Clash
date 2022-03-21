using System.Collections;
using UnityEngine;
using Zenject;
using Obi;

namespace Softbody
{
    [RequireComponent(typeof(ObiSolver),typeof(ObiFixedUpdater))]
    public abstract class SoftbodyLogic : MonoBehaviour
    {
        [SerializeField] private Vector3 _scale;
        
        protected ObiSolver _solver;
        protected ObiFixedUpdater _obiFixedUpdater;
        
        private IEnumerator Start()
        {
            _solver = GetComponent<ObiSolver>();
            _obiFixedUpdater = GetComponent<ObiFixedUpdater>();
            
            yield return new WaitForEndOfFrame();
            transform.localScale = _scale;

            _solver.enabled = false;
            _obiFixedUpdater.enabled = false;
        }
        
        public class Factory: PlaceholderFactory<GameObject,SoftbodyLogic>{}
    }
}