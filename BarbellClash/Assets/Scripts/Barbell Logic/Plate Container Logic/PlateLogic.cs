using System.Linq;
using UnityEngine;

namespace Barbell
{
    public class PlateLogic : MonoBehaviour, ISizeable
    {
        [SerializeField] private MeshRenderer[] _renderers;
        [SerializeField] private PlateData _data;

        public Transform[] scaleElement => _renderers
            .Select(r => r.transform)
            .ToArray<Transform>();
        
        [field: SerializeField]
        public float Radius { get; private set; }
        
        [field: SerializeField]
        public float Thickness { get; private set; }

        public uint Id => _data.Id;

        public float RotateSpeed => _data.RotateSpeed;
    }
}