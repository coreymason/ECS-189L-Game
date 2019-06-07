using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] public GameObject playerPrefab;
    
    public override void InstallBindings()
    {
        Container.Bind<AudioManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<WorldManager>().FromComponentInHierarchy().AsSingle();
        
//        Container.Bind<Player>().FromComponentInHierarchy().WhenInjectedInto<PlayerController>();
//        Container.Bind<Player>().FromComponentsInHierarchy().;
        
        Container.BindFactory<Player, Player.Factory>().FromSubContainerResolve()
            .ByNewContextPrefab(playerPrefab).UnderTransformGroup("WorldManager/Players");
//        Container.BindFactory<Enemy, Enemy.Factory>().FromSubContainerResolve()
//            .ByNewContextPrefab(enemyPrefab).UnderTransformGroup("WorldManager/EnemyManager");

        Container.Bind<HealthCounterDisplay>().FromComponentInHierarchy().AsSingle();

        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<PlayerHealthSignal>();
        Container.BindSignal<PlayerHealthSignal>()
            .ToMethod<HealthCounterDisplay>(x => x.UpdateHealthText).FromResolve();
    }
}