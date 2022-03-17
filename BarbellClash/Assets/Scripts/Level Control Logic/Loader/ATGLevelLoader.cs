using UnityEngine;
using Zenject;

namespace ATG.LevelControl
{
    public class ATGLevelLoader : MonoBehaviour
    {
        [Inject]
        private void Constructor(ILevelSystem levelSystem)
        {
            levelSystem.LoadLevel();
        }

        public void GoToNeededLevel(int neededLevel)
        {
            string link = ATGLevelSystem.LevelInfoRef;
            PlayerPrefs.SetInt(link,neededLevel);
            
            Debug.LogWarning(
                $"Player Prefs data set level to <color=red>{neededLevel}</color>");
        }

        public void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            
            Debug.LogWarning("All Player Prefs is cleared");
        }
    }
}
