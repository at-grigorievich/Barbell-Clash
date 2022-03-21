using System;
using System.Collections;
using Obi;
using UnityEngine;

namespace Softbody.Types
{
    [RequireComponent(typeof(ObiSolver),typeof(ObiFixedUpdater))]
    public class BigAssSoftbody: SoftbodyLogic
    {
        [SerializeField] private Vector3 _scale;

        private ObiSolver _solver;
        private ObiFixedUpdater _obiFixedUpdater;
        
        private IEnumerator Start()
        {
            _solver = GetComponent<ObiSolver>();
            _obiFixedUpdater = GetComponent<ObiFixedUpdater>();
            
            yield return new WaitForEndOfFrame();
            transform.localScale = _scale;

            //_solver.enabled = false;
            //_obiFixedUpdater.enabled = false;
        }
    }
}