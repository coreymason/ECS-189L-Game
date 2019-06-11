using System;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wander : PeacefulAI
{
    [SerializeField] private float wanderMaxDistance = 20f;
    [SerializeField] private float wanderMinWaitTime = 0f;
    [SerializeField] private float wanderMaxWaitTime = 10f;
    [SerializeField] private float miniWanderMaxDistance = 1.3f;
    [SerializeField] private float miniWanderMinWaitTime = 1f;
    [SerializeField] private float miniWanderMaxWaitTime = 2.5f;
    
    private float _wanderTimer;
    private float _miniWanderTimer;

    private float _wanderWaitTime;
    private float _miniWanderWaitTime;

    private bool _isWandering = false;
    private bool _isMiniWandering = false;

    private IAstarAI _ai;
    
    private new void Start()
    {
        base.Start();
        
        _ai = GetComponent<IAstarAI>();
        _wanderTimer = 0f;
        _wanderWaitTime = 0f;
        _miniWanderTimer = 0f;
        _miniWanderWaitTime = 0f;
    }
    
    Vector3 PickRandomPoint (float limitRadius) {
        var point = Random.insideUnitSphere * limitRadius;

        point.z = 0;
        point += _ai.position;
        return point;
    }
    
    Vector3 PickRandomAIWalkableNode () {
        var grid = AstarPath.active.data.gridGraph;
        var currentNode = AstarPath.active.GetNearest(transform.position, NNConstraint.Default).node;
        var randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];

        while (!PathUtilities.IsPathPossible(currentNode, randomNode))
        {
            randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];
        }
        
        return (Vector3)randomNode.position;
    }
    
    Vector3 PickBFSNode (int maxDistance) {
        var startNode = AstarPath.active.GetNearest(transform.position, NNConstraint.Default).node;
        var nodes = PathUtilities.BFS(startNode, maxDistance);
        
        return PathUtilities.GetPointsOnNodes(nodes, 1)[0];
    }
    
    private void Update ()
    {
        bool restartedWander = false;
        bool restartedMiniWander = false;
            
        // If ai does not have a path
        if (!_ai.pathPending && (_ai.reachedEndOfPath || !_ai.hasPath))
        {
            // Reset active wandering trackers
            _isWandering = false;
            _isMiniWandering = false;
            
            // Attempt to wander
            restartedWander = TryWander();
            // If failed, attempt to mini wander
            if (!restartedWander)
            {
                restartedMiniWander = TryMiniWander();
            }
        }
        // If ai does have a path but it's a mini wander, attempt canceling for a big wander
        else if(_isMiniWandering)
        {
            restartedWander = TryWander();
        }

        // Add to timers
        if (!restartedWander && !_isWandering)
        {
            _wanderTimer += Time.deltaTime;
        }
        if (!restartedMiniWander && !_isWandering && !_isMiniWandering)
        {
            _miniWanderTimer += Time.deltaTime;
        }
    }

    private bool TryWander()
    {
        if(_wanderTimer >= _wanderWaitTime)
        {
            Debug.Log("big wander!");
            _ai.destination = PickBFSNode((int)wanderMaxDistance);
            _ai.SearchPath();
            

            _wanderTimer = 0f;
            _miniWanderTimer = 0f;
            _wanderWaitTime = Random.Range(wanderMinWaitTime, wanderMaxWaitTime);
            _isWandering = true;
            _isMiniWandering = false;

            return true;
        }

        return false;
    }
    
    private bool TryMiniWander()
    {
        if(_miniWanderTimer >= _miniWanderWaitTime)
        {
            Debug.Log("mini wander!");
            _ai.destination = PickRandomPoint(miniWanderMaxDistance);
            _ai.SearchPath();
            
            _miniWanderTimer = 0f;
            _miniWanderWaitTime = Random.Range(miniWanderMinWaitTime, miniWanderMaxWaitTime);
            _isMiniWandering = true;
            _isWandering = false;

            return true;
        }

        return false;
    }
}