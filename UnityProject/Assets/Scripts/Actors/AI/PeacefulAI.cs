using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(AIPath))]
public abstract class PeacefulAI : MonoBehaviour
{
    protected AIPath AiPath;
    
    protected void Start()
    {
        AiPath = GetComponent<AIPath>();
    }
}