using System;
using Obi;
using UnityEngine;

namespace Softbody
{
    public class SoftbodyCrushVisualizer : MonoBehaviour
    {
        [SerializeField] private ObiSoftbody _softbody;

        private void Start()
        {
            var a = _softbody.softbodyBlueprint as ObiSoftbodySurfaceBlueprint;
            Debug.Log(a.vertexToParticle.Length);
        }
    }
}