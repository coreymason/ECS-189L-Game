using UnityEngine;
using Zenject;

[RequireComponent(typeof(PeacefulAI))]
[RequireComponent(typeof(HostileAI))]
public class EnemyController : MonoBehaviour
{
    private Enemy _enemy;
    private PeacefulAI _peacefulAI;
    private HostileAI _hostileAI;

//    [Inject]
//    private void Init(Enemy enemy)
//    {
//        _enemy = enemy;
//    }

    private void Start()
    {
        _peacefulAI = GetComponent<PeacefulAI>();
        _hostileAI = GetComponent<HostileAI>();
        
        _peacefulAI.enabled = true;
        _hostileAI.enabled = false;

    }

    public void FoundTarget(Transform target)
    {
        _hostileAI.SetTarget(target);
        _peacefulAI.enabled = false;
        _hostileAI.enabled = true;
        _hostileAI.UpdateAIPathSettings();
    }
}
