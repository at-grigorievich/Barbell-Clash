using System;
using UnityEngine;

namespace ATG.LevelControl
{
    [CreateAssetMenu(fileName = "Level Data", menuName = "Levels/New Linear Level", order = 0)]
    public class LinearLevelData: LevelData
    {
        [Space(10)]
        [SerializeField] private LineLevelType _levelType;
        [Space(10)]
        [SerializeField] private EnvironmentBlock[] _blocks;

        public override LevelType TypeOfLevel => (LevelType)Enum.Parse(typeof(LevelType),_levelType.ToString());
        
        
        public override T[] GetAnsInstantiateLevelBlocks<T>(ICreateLevelBehaviour createLevel)
        {
            if (createLevel == null)
                throw new NullReferenceException("CreateLevelBehaviour is null");
            
            var levelParent = CreateSceneDataObjects();
            return createLevel.InstantiateBlocks<T>(_blocks, levelParent);
        }
    }
}