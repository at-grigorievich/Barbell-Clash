using Softbody.Interfaces;
using UnityEngine;
using Zenject;

namespace Softbody
{
    public class AnimatorInstaller: MonoInstaller
    {
        [SerializeField] private SoftbodyAnimator _animator;

        public override void InstallBindings()
        {
            Container.Bind<IAnimator>().FromInstance(_animator).AsSingle().NonLazy();
        }
    }
}