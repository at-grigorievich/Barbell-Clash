using UnityEngine;
using Zenject;

namespace VFXLogic
{
    [CreateAssetMenu(fileName = "VFXDataInstaller", menuName = "Installers/VFXDataInstaller")]
    public class VFXDataInstaller : ScriptableObjectInstaller<VFXDataInstaller>
    {
        [SerializeField] private VFXData dataList;

        public override void InstallBindings()
        {
            Container
                .BindInstance(dataList)
                .AsSingle()
                .NonLazy();
        }
    }
}