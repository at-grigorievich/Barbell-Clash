using Zenject;

namespace Barbell
{
    public class BarbellFactoryInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindFactory<UnityEngine.Object, BarbellLogic, BarbellLogic.Factory>()
                .FromFactory<PrefabFactory<BarbellLogic>>();
        }
    }
}