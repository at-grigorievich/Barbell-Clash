using UnityEngine;
using Zenject;

namespace PlayerLogic.Zenject
{
    public class BoostServiceInstaller: MonoInstaller
    {
        [SerializeField] private PlayerLogicService _boostInstance;

        public override void InstallBindings()
        {
            Container.Bind<IBoostable>()
                .FromInstance(_boostInstance)
                .AsSingle().NonLazy();
        }
    }
}