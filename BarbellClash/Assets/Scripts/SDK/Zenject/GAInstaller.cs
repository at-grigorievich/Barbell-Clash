using UnityEngine;
using Zenject;

namespace ATG.SDK.Zenject
{
    public class GAInstaller: MonoInstaller
    {
        [SerializeField] private GameAnalyticService _gaService;

        public override void InstallBindings()
        {
            Container.Bind<GameAnalyticService>()
                .FromComponentInNewPrefab(_gaService)
                .AsSingle().NonLazy();
        }
    }
}