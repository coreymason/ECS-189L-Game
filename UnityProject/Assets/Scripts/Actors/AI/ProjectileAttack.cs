using UnityEngine;

public class ProjectileAttack : HostileAI
{
    [SerializeField] private Projectile projectilePrefab;

    public override void UpdateAIPathSettings()
    {
        AiPath.endReachedDistance = 6f;
        AiDestinationSetter.target = Target.transform;
    }

    private void Update()
    {
        
    }
}