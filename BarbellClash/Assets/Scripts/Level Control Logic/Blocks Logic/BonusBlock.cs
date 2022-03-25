using UnityEngine;

namespace ATG.LevelControl
{
    public class BonusBlock: EnvironmentBlock
    {
        [field: SerializeField]
        public int BoostIndex { get; private set; }
        
        [field: SerializeField]
        public float NeedSpeed { get; private set; }
    }
}