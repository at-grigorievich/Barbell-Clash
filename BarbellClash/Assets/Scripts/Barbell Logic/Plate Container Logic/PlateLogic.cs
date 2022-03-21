using UnityEngine;

namespace Barbell
{
    public class PlateLogic : MonoBehaviour, ISizeable
    {
        [SerializeField] private PlateData _data;

        [field: SerializeField]
        public float Radius { get; private set; }
        
        [field: SerializeField]
        public float Thickness { get; private set; }

        public uint Id => _data.Id;
        
    }
}