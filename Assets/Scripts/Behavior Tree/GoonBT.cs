using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BTArchitecture;

public class GoonBT : BehaviorTree
{
    protected override Node SetupTree() {
        GameObject target = GameObject.FindWithTag("Player");
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        Node root = new SelectorNode(new List<Node>{
            new UnableToMoveNode(),
            new SequenceNode(new List<Node>{
                new DetectPlayerNode(),
                new StopAgentNode(),
            }),
            new FollowPlayerNode(target),
        });
        root.SetData("gameobject", this.gameObject);
        root.SetData("entity", GetComponent<Entity>());
        root.SetData("agent", agent);
        root.SetData("controller", GetComponent<Controller>());

        return root;
    }
}
