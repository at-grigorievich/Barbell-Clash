using UnityEngine;
using Zenject;

namespace Softbody
{
    public class SoftbodyFactoryInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindFactory<GameObject, SoftbodyLogic, SoftbodyLogic.Factory>()
                .FromFactory<PrefabFactory<SoftbodyLogic>>();
        }
    }
}