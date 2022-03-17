using UnityEngine;

namespace ATG.LevelControl
{
    public enum LevelType
    {
        Line,
        Matrix,
        ZenjectLine,
        ZenjectMatrix,
        Static,
        ZenjectStatic
    }
    public enum LineLevelType
    {
        Line,
        Static,
        ZenjectLine,
        ZenjectStatic
    }

    public enum MatrixLevelType
    {
        Matrix,
        ZenjectMatrix
    }
    
    public abstract class LevelData : ScriptableObject, ILevel
    {
        [SerializeField] private int id;
        [SerializeField] private new string name;
        
        public int Id => id;
        public string Name => name;
        
        public abstract LevelType TypeOfLevel { get;}


        public abstract T[] GetAnsInstantiateLevelBlocks<T>(ICreateLevelBehaviour createLevel);

        protected GameObject CreateSceneDataObjects() => new GameObject("----SCENE----");
    }
}