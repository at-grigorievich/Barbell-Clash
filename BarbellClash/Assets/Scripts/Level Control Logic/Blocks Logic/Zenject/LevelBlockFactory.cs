using ATG.LevelControl;
using UnityEngine;
using Zenject;

public class LevelBlockFactory : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .BindFactory<GameObject, EnvironmentBlock, EnvironmentBlock.Factory>()
            .FromFactory<PrefabFactory<EnvironmentBlock>>();
    }
}