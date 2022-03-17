using System;
using System.Collections.Generic;
using System.Linq;
using Array2DEditor;
using UnityEngine;

namespace ATG.LevelControl
{
    [Serializable]
    public class PrefabById
    {
        [SerializeField] private int id;
        [SerializeField] private EnvironmentBlock blockPrefab;

        public int Id => id;
        public EnvironmentBlock Block => blockPrefab;
    }
    

    [CreateAssetMenu(fileName = "Level Data", menuName = "Levels/New Matrix Level", order = 0)]
    public class MatrixLevelData: LevelData
    {
        [Space(10)]
        [SerializeField] private MatrixLevelType _levelType;
        [Space(10)]
        [SerializeField] private PrefabById[] prefabsId;
        [SerializeField] private Array2DInt matrixData;
        
        public override LevelType TypeOfLevel => (LevelType)Enum.Parse(typeof(LevelType),_levelType.ToString());

        public override T[] GetAnsInstantiateLevelBlocks<T>(ICreateLevelBehaviour createLevel)
        {
            if (createLevel == null)
                throw new NullReferenceException("CreateLevelBehaviour is null");
            
            List<T> arr = new List<T>();
            var parent = CreateSceneDataObjects();
            
            for(int i = 0;i < matrixData.GridSize.y;i++)
            {
                var line = new EnvironmentBlock[matrixData.GridSize.x];
                for (int j = 0; j < matrixData.GridSize.x; j++)
                {
                    var prefabId = matrixData.GetCell(j, i);
                    var el = prefabsId.FirstOrDefault(el => el.Id == prefabId);
                    
                    line[j] = el?.Block;
                }

                arr.AddRange(createLevel.InstantiateBlocks<T>(line, parent));
            }

            return arr.ToArray();
        }
    }
}