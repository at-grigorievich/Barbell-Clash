using UILogic;
using UnityEngine;
using Zenject;

public class UISystemInstaller : MonoInstaller
{
    [SerializeField] private UIControlSystem controlSystem;

    public override void InstallBindings()
    {
        Container.Bind<UIControlSystem>().FromInstance(controlSystem).AsSingle().NonLazy();
        Container.Bind<IPanel>().FromComponentsInHierarchy().AsSingle().NonLazy();
    }
}