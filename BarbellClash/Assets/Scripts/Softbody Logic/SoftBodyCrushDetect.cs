using Obi;
using UnityEngine;

namespace Softbody
{
    [RequireComponent(typeof(SkinnedMeshRenderer),typeof(ObiSoftbody))]
    public class SoftBodyCrushDetect : MonoBehaviour
    {
        [SerializeField] private Color _crushColor;
        [SerializeField] private float _colorSpeed;
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
        
        public void ColorMainSoftbodyPart()
        {
            Color[] colors = _renderer.sharedMesh.colors;
            
            for (var i = 0; i < _skinner.m_softbodyInfluences.Length; i++)
            {
                if (_skinner.m_softbodyInfluences[i] >= _limit)
                {
                    colors[i] = Color.Lerp(colors[i],_crushColor,_colorSpeed*Time.deltaTime);
                }
            }

            _renderer.sharedMesh.colors = colors;
        }
    }
}