using UnityEngine;

namespace Barbell
{
    public class PlateLogic : MonoBehaviour, ISizeable
    {
        [field: SerializeField]
        public float Radius { get; private set; }
        
        [field: SerializeField]
        public float Thickness { get; private set; }

        private void Start()
        {
        }
    }
}