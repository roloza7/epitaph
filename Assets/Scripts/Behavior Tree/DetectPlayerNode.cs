using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTArchitecture;

public class DetectPlayerNode : Node
{
    // detects if the enemy is colliding with the player

    private EnemyController _enemyController;
    
    public DetectPlayerNode() {
    }

    public override NodeState Evaluate() {
        if (_enemyController == null) {
            _enemyController = (EnemyController)this.GetData("controller");
        }
        if (_enemyController.IsColliding) {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
