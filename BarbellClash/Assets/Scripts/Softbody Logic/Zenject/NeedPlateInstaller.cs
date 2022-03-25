using UnityEngine;
using Zenject;

namespace Softbody
{
    public class NeedPlateInstaller: MonoInstaller
    {
        [SerializeField] private SoftbodyLogic _sl;

        public override void InstallBindings()
        {
            Container.Bind<uint>()
                .WithId("NeedPlate")
                .FromInstance(_sl.NeededPlateId)
                .AsSingle()
                .NonLazy();
        }
    }
}