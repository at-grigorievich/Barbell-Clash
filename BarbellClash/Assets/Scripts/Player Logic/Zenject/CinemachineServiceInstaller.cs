using UnityEngine;
using Zenject;

namespace PlayerLogic.Zenject
{
    public class CinemachineServiceInstaller: MonoInstaller
    {
        [SerializeField] private PlayerCinemachineService _cinemachine;
        
        public override void InstallBindings()
        {
            Container.Bind<ICinemachinable>()
                .FromInstance(_cinemachine)
                .AsSingle()
                .NonLazy();
        }
    }
}