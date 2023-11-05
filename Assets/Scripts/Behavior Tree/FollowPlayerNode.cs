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
    private float _movespeed;

    public FollowPlayerNode(GameObject target) {
        _target = target;

    }

    public override NodeState Evaluate() {
        if (_agent == null) {
            _agent = (NavMeshAgent)this.GetData("agent");
        }
        _movespeed = ((Entity)this.GetData("entity")).EntityStats.GetStatValue(StatEnum.WALKSPEED);
        _agent.enabled = true;
        _agent.isStopped = false;
        _agent.SetDestination(_target.transform.position);
        _agent.speed = _movespeed;
        Debug.Log("hoi");
        return NodeState.SUCCESS;
    }
}
