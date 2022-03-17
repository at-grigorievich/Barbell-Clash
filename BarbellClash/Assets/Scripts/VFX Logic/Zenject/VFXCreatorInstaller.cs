using UnityEngine;
using Zenject;

namespace VFXLogic
{
    public class VFXCreatorInstaller : MonoInstaller
    {
        [SerializeField] private VFXCreatorService creatorService;
        
        public override void InstallBindings()
        {
            Container
                .Bind<IVFXControllable>()
                .FromInstance(creatorService)
                .AsSingle();
        }
    }
}