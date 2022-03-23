using Obi;
using UnityEngine;

namespace Softbody
{
    [RequireComponent(typeof(SkinnedMeshRenderer),typeof(ObiSoftbody))]
    public class SoftBodyCrushDetect : MonoBehaviour
    {
        [SerializeField] private float _limit;

        private SkinnedMeshRenderer _renderer;
        private ObiSoftbodySkinner _skinner;

        private Color[] _defaultColors;
        
        private void Start()
        {
            _renderer = GetComponent<SkinnedMeshRenderer>();
            _skinner = GetComponent<ObiSoftbodySkinner>();
            
            _defaultColors = (Color[])_renderer.sharedMesh.colors.Clone();
        }

        private void OnDisable()
        {
            _renderer.sharedMesh.colors = _defaultColors;
        }
        
        public void ColorMainSoftbodyPart(Color color)
        {
            Color[] colors = (Color[]) _renderer.sharedMesh.colors.Clone();

            for (var i = 0; i < _skinner.m_softbodyInfluences.Length; i++)
            {
                if (_skinner.m_softbodyInfluences[i] >= _limit)
                {
                    colors[i] = color;
                }
            }

            _renderer.sharedMesh.colors = colors;
        }
    }
}