using UnityEngine;
using Zenject;

namespace Softbody
{
    public class MainPartInstaller: MonoInstaller
    {
        [SerializeField] private SoftbodyMainPart _mainPart;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_mainPart).AsSingle().NonLazy();
        }
    }
}