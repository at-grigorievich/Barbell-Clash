using Zenject;

namespace ATG.LevelControl
{
    public class LevelSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStatus();
            BindSystem();
        }

        private void BindSystem()
        {
            Container
                .Bind<ILevelSystem>()
                .To<ATGLevelSystem>()
                .AsSingle();
        }
        private void BindStatus()
        {
            Container
                .Bind<ILevelStatus>()
                .To<ATGLevelStatus>()
                .AsSingle();
        }
    }
}