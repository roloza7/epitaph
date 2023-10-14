using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BTArchitecture
{
    public class BossSpawnEnemyNode : Node
    {
        public BossSpawnEnemyNode() { }

        public override NodeState Evaluate()
        {
            return NodeState.SUCCESS;
        }
    }
}
