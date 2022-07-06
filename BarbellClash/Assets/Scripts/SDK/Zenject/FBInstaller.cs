using UnityEngine;
using Zenject;

namespace ATG.SDK.Zenject
{
    public class FBInstaller: MonoInstaller
    {
        [SerializeField] private FacebookService _fb;

        public override void InstallBindings()
        {
            Container.Bind<FacebookService>()
                .FromComponentInNewPrefab(_fb)
                .AsSingle().NonLazy();
        }
    }
}