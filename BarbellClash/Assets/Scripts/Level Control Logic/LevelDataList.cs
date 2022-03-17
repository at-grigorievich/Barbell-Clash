using System;
using System.Collections.Generic;
using UnityEngine;

namespace ATG.LevelControl
{
    [CreateAssetMenu(fileName = "Level Data List", menuName = "Levels/New Levels List", order = 0)]
    public class LevelDataList : ScriptableObject
    {
        [SerializeField] private List<LevelData> levels;
        public int LevelsCount => levels.Count;
        
        public ILevel FindLevelData(int levelId)
        {
            if (levels == null || levels.Count == 0)
                throw new IndexOutOfRangeException("levels array is incorrect !");
            
            if (levelId >= 0)
            {
                LevelData selectedLevel = levels.Find(level => level.Id == levelId);
                if (selectedLevel != null)
                {
                    return selectedLevel;
                }
                
                throw new NullReferenceException($"Cant find level with id {levelId}");
            }
            throw new IndexOutOfRangeException("level id must be non negative !");
        }
    }
}