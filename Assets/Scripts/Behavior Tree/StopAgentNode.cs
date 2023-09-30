using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BTArchitecture;


public class StopAgentNode : Node
{
    // stops the agent and automatically returns success
    private GameObject _self;
    private NavMeshAgent _agent;

    public StopAgentNode() {
    }

    public override NodeState Evaluate() {
        if (_agent == null) {
            _agent = (NavMeshAgent)this.GetData("agent");
        }
        if (_self == null) {
            _self = (GameObject)this.GetData("gameobject");
        }
        _self.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        _agent.enabled = true;
        _agent.isStopped = true;
        return NodeState.SUCCESS;
    }
}
