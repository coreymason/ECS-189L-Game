using UnityEngine;
using Zenject;

public class CrosshairInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Crosshair>().FromComponentInHierarchy().AsSingle();
        Container.Bind<InputManager>().FromComponentInHierarchy().AsSingle();
    }
}