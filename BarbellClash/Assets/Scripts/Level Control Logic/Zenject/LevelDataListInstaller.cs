using UnityEngine;
using Zenject;

namespace ATG.LevelControl
{
    [CreateAssetMenu(fileName = "LevelDataListInstaller", menuName = "Installers/LevelDataListInstaller")]
    public class LevelDataListInstaller : ScriptableObjectInstaller<LevelDataListInstaller>
    {
        [SerializeField] private LevelDataList levelDataList;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(levelDataList)
                .AsSingle()
                .NonLazy();
        }
    }
}