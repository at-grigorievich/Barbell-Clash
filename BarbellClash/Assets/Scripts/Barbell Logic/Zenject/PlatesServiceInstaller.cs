using UnityEngine;
using Zenject;

namespace Barbell.Zenject
{
    public class PlatesServiceInstaller: MonoInstaller
    {
        [SerializeField] private BarbellPlatesService[] _leftPlateContainer;

        public override void InstallBindings()
        {
            Container.Bind<IPlateContainer[]>()
                .FromInstance(_leftPlateContainer)
                .AsSingle().NonLazy();
        }

        private void BindPlate(BarbellPlatesService platesService)
        {
            Container.Bind<IPlateContainer>()
                .To<BarbellPlatesService>()
                .FromInstance(platesService)
                .AsSingle()
                .NonLazy();
        }
    }
}