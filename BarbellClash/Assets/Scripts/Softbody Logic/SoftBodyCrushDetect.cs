using System;
using Obi;
using UnityEngine;

namespace Softbody
{
    [RequireComponent(typeof(SkinnedMeshRenderer),typeof(ObiSoftbody))]
    public class SoftBodyCrushDetect : MonoBehaviour
    {
        private SkinnedMeshRenderer _renderer;
        private ObiSoftbody _currentSoftbody;
        private ObiSoftbodySkinner _skinner;

        private Color[] _colors, _defaultColors;
        
        private void Start()
        {
            _renderer = GetComponent<SkinnedMeshRenderer>();
            _currentSoftbody = GetComponent<ObiSoftbody>();
            _skinner = GetComponent<ObiSoftbodySkinner>();
            
            //_currentSoftbody.OnEndStep += CurrentSoftbodyOnOnEndStep;
            //_currentSoftbody.solver.OnCollision += SolverOnOnCollision;

            _colors = new Color[_renderer.sharedMesh.colors.Length];
            _defaultColors = (Color[])_renderer.sharedMesh.colors.Clone();
            
            Debug.Log(_skinner.m_softbodyInfluences.Length);
            Debug.Log(_renderer.sharedMesh.colors.Length);
            
            for (var i = 0; i < _skinner.m_softbodyInfluences.Length; i++)
            {
                if (_skinner.m_softbodyInfluences[i] > 0.8f)
                {
                    _colors[i] = Color.green;
                }
            }

            _renderer.sharedMesh.colors = _colors;
        }

        private void OnDisable()
        {
            _renderer.sharedMesh.colors = _defaultColors;
        }

        private void CurrentSoftbodyOnOnEndStep(ObiActor actor, float steptime)
        {
            //_renderer.sharedMesh.colors = _colors;
        }

        private void SolverOnOnCollision(ObiSolver solver, ObiSolver.ObiCollisionEventArgs e)
        {
            foreach (Oni.Contact contact in e.contacts)
            {
                if (contact.distance < 0.01)
                {
                    int particleIndex = _currentSoftbody.solverIndices[contact.bodyA];

                    Vector3 position = _currentSoftbody.solver.positions[particleIndex];
                    position = _currentSoftbody.GetParticlePosition(particleIndex);

                    if (_skinner.m_softbodyInfluences[particleIndex] > 0.8f)
                    {
                        Debug.Log(_skinner.m_softbodyInfluences[particleIndex]);
                    }
                    //var b = new GameObject("t");
                    //b.transform.position = position;
                }
            }
        }
    }
}