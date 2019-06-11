using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(AIPath))]
public class PeacefulAI : MonoBehaviour
{
    protected AIPath AiPath;
    
    protected void Start()
    {
        AiPath = GetComponent<AIPath>();
    }
}