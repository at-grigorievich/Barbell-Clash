using Zenject;

namespace ATG.PlayerData
{
    public class PlayerDataInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<PlayerData>()
                .AsSingle()
                .NonLazy();
        }
    }
}