using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ATG.LevelControl
{
    public class ATGLevelSystem: ILevelSystem, IInitializable
    {
        public const string LevelInfoRef = "Level_ID";

        [Inject] private LevelDataList _levelDataList;
        [Inject] private PlayerData.PlayerData _playerData;
        [Inject] private ILevelStatus _levelStatus;

        [Inject] private EnvironmentBlock.Factory _blockFactory;
        
        public int CurrentLevelId => PlayerPrefs.HasKey(LevelInfoRef) ? PlayerPrefs.GetInt(LevelInfoRef) : 1;
        
        public ILevel CurrentLevel => _levelDataList.FindLevelData(CurrentLevelId);
        public EnvironmentBlock[] BlockInstances { get; private set; }
        

        [Inject]
        public virtual void Initialize()
        {
            _levelStatus.OnCompleteLevel += (sender, args) =>
            {
                SaveLevel(CurrentLevelId + 1);
                _playerData.UpdateLevel();
            };
        }
        
        public virtual void LoadLevel()
        {
            ICreateLevelBehaviour levelBehaviour =
                CreateBehaviourFactory.Execute(CurrentLevel.TypeOfLevel, _blockFactory);
            
            BlockInstances = 
                CurrentLevel.GetAnsInstantiateLevelBlocks<EnvironmentBlock>(levelBehaviour);
        }
        
        public virtual void LoadNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        
        public void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void SaveLevel(int level)
        {
            int savedLevel = level > 0 && level <= _levelDataList.LevelsCount ? level : 1;
            PlayerPrefs.SetInt(LevelInfoRef,savedLevel);
        }
    }
}