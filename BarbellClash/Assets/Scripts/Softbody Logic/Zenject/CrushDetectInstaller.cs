using UnityEngine;
using Zenject;

namespace Softbody
{
    public class CrushDetectInstaller: MonoInstaller
    {
        [SerializeField] private SoftBodyCrushDetect _crushDetect;

        public override void InstallBindings()
        {
            Container.BindInstance(_crushDetect)
                .AsSingle()
                .NonLazy();
        }
    }
}