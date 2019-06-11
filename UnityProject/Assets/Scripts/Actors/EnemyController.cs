using Pathfinding;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PeacefulAI))]
[RequireComponent(typeof(HostileAI))]
[RequireComponent(typeof(AIPath))]
public class EnemyController : MonoBehaviour
{
    private Enemy _enemy;
    private PeacefulAI _peacefulAI;
    private HostileAI _hostileAI;
    
    private AIPath _aiPath;
    
    public bool IsMovingRight { get; private set; }
    
//    [Inject]
//    private void Init(Enemy enemy)
//    {
//        _enemy = enemy;
//    }

    private void Start()
    {
        _peacefulAI = GetComponent<PeacefulAI>();
        _hostileAI = GetComponent<HostileAI>();
        _aiPath = GetComponent<AIPath>();
        
        _peacefulAI.enabled = true;
        _hostileAI.enabled = false;

        IsMovingRight = true;
    }

    private void Update()
    {
        if (_aiPath.velocity.x < 0)
        {
            IsMovingRight = false;
        }
        else if (_aiPath.velocity.x > 0)
        {
            IsMovingRight = true;
        }
    }

    public void FoundTarget(Transform target)
    {
        _hostileAI.SetTarget(target);
        _peacefulAI.enabled = false;
        _hostileAI.enabled = true;
        _hostileAI.UpdateAIPathSettings();
    }
}
