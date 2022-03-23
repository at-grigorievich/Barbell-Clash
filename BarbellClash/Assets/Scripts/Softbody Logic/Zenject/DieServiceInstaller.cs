using Softbody.Interfaces;
using UnityEngine;
using Zenject;

namespace Softbody
{
    public class DieServiceInstaller: MonoInstaller
    {
        [SerializeField] private DieInteractable _dieInteractiveService;

        public override void InstallBindings()
        {
            Container.Bind<IDieInteractable>()
                .FromInstance(_dieInteractiveService)
                .AsSingle()
                .NonLazy();
        }
    }
}