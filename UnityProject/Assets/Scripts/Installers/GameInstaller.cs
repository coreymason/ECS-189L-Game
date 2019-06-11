using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] public GameObject playerPrefab;
    [SerializeField] public GameObject arrowPrefab;
    
    public override void InstallBindings()
    {
        Container.Bind<AudioManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<WorldManager>().FromComponentInHierarchy().AsSingle();
        
        
        Container.BindFactory<Player, Player.Factory>().FromSubContainerResolve()
            .ByNewContextPrefab(playerPrefab).UnderTransformGroup("WorldManager/Players");
        Container.BindFactory<UnityEngine.Object, Vector3, Quaternion, Vector2, Projectile, Projectile.Factory>()
                .FromFactory<ProjectileFactory>();
//        Container.BindFactory<Enemy, Enemy.Factory>().FromSubContainerResolve()
//            .ByNewContextPrefab(enemyPrefab).UnderTransformGroup("WorldManager/EnemyManager");


        SignalBusInstaller.Install(Container);
        
        Container.DeclareSignal<PlayerHealthSignal>();
        Container.Bind<HealthCounterDisplay>().FromComponentInHierarchy().AsSingle();
        Container.BindSignal<PlayerHealthSignal>()
            .ToMethod<HealthCounterDisplay>(x => x.UpdateHealthText).FromResolve();
        
        Container.DeclareSignal<CameraFollowTargetSignal>();
        Container.Bind<CameraRig>().FromComponentInHierarchy().AsSingle();
        Container.BindSignal<CameraFollowTargetSignal>()
            .ToMethod<CameraRig>(x => x.UpdateFollowTarget).FromResolve();
    }
}