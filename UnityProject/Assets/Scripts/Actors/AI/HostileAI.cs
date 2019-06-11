using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(AIDestinationSetter))]
public abstract class HostileAI : MonoBehaviour
{
    protected AIPath AiPath;
    protected AIDestinationSetter AiDestinationSetter;
    protected Transform Target;

    protected void Start()
    {
        AiPath = GetComponent<AIPath>();
        AiDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    public abstract void UpdateAIPathSettings();
}