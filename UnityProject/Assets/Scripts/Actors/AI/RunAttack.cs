using Pathfinding;
using UnityEngine;

public class RunAttack : HostileAI
{
    
    public override void UpdateAIPathSettings()
    {
        AiPath.repathRate = 1f;
        AiPath.maxSpeed = 12f;
        AiPath.slowdownDistance = 1f;
        AiDestinationSetter.target = Target.transform;
    }
}
