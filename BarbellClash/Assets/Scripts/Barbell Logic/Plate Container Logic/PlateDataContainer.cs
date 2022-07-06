using UnityEngine;

namespace Barbell
{
    [CreateAssetMenu(fileName = "Plates", menuName = "Plate Data/New Plate Data", order = 0)]
    public class PlateDataContainer : ScriptableObject
    {
        [field: SerializeField]
        public PlateLogic[] Plates { get; private set; }
    }
}