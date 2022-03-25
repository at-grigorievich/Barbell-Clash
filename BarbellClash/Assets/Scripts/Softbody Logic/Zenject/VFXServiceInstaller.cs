using Softbody.Interfaces;
using UnityEngine;
using Zenject;

namespace Softbody
{
    public class VFXServiceInstaller: MonoInstaller
    {
        [SerializeField] private SoftbodyVFXService _vfxService;
        
        public override void InstallBindings()
        {
            Container.Bind<IVisualable>().FromInstance(_vfxService)
                .AsSingle().NonLazy();
        }
    }
}