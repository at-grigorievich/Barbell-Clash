using UnityEngine;

namespace Barbell
{
    [CreateAssetMenu(fileName = "Plates", menuName = "Plate Data/New Plate Parameters", order = 0)]
    public class PlateData : ScriptableObject
    {
        [field: Range(0,100)]
        [field: SerializeField]
        public uint Id { get; private set; }
        
        [field: Space(10)]
        [field: SerializeField]
        public float RotateSpeed { get; private set; }
    }
}