using UnityEngine;
using Zenject;

namespace Debrief.Zenject
{
    public class BonusDetectorInstaller: MonoInstaller
    {
        [SerializeField] private DebriefServiceLogic _debrief;
        
        public override void InstallBindings()
        {
            Container.Bind<IBonusDetector>()
                .FromInstance(_debrief)
                .AsSingle().NonLazy();
        }
    }
}