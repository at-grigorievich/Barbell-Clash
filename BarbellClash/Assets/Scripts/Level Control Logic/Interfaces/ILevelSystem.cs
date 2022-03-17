namespace ATG.LevelControl
{
    public interface ILevelSystem
    {
        int CurrentLevelId { get; }
        ILevel CurrentLevel { get; }
        EnvironmentBlock[] BlockInstances { get; }
        
        
        void LoadLevel();
        void LoadNextLevel();

        void ReloadLevel();
        void SaveLevel(int level);
    }
}
