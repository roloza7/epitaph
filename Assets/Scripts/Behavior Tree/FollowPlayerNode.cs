using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BTArchitecture;

public class FollowPlayerNode : Node
{
    
    // follows the player and always returns success

    private GameObject _target;
    private NavMeshAgent _agent;
    private float _speed;

    public FollowPlayerNode(GameObject target, UnityEngine.AI.NavMeshAgent agent, float speed) {
        _speed = speed;
        _target = target;
        _agent = agent;
    }

    public override NodeState Evaluate() {
        _agent.isStopped = false;
        _agent.SetDestination(_target.transform.position);
        _agent.speed = _speed;
        return NodeState.SUCCESS;
    }
}
