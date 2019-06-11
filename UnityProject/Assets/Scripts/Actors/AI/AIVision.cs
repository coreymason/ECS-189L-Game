using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

[RequireComponent(typeof(EnemyController))]
public class AIVision : MonoBehaviour
{
    private EnemyController _enemyController;

    [SerializeField] [Range(0,360)] public float viewRadius;
    [SerializeField] public float viewAngle;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    [HideInInspector] public List<Transform> foundTargets;

//    [Inject]
//    private void Init(EnemyController enemyController)
//    {
//        _enemyController = enemyController;
//    }

    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();
        foundTargets = new List<Transform>();
        
        StartCoroutine(nameof(FindTargetsOnDelay), .2f);
    }

    public Vector2 DirectionFromAngle(float angle, bool isGlobal)
    {
        if (!isGlobal)
        {
            angle -= transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    private IEnumerator FindTargetsOnDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisisbleTargets();

            if (foundTargets.Count > 0)
            {
                _enemyController.FoundTarget(foundTargets[0]);
                foundTargets.Clear();
                yield break;
            }
        }
    }

    private void FindVisisbleTargets()
    {
        foundTargets.Clear();
        Collider2D[] targetsInView = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);
        
        foreach (var targetCollider in targetsInView)
        {
            Transform target = targetCollider.transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector2.Angle(transform.up, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    foundTargets.Add(target);
                }
            }
        }
    }
}
