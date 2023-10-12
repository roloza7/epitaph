using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTArchitecture
{
    public class BossMeteorNode : Node
    {
        public BossMeteorNode() { }

        public override NodeState Evaluate()
        {
            return NodeState.SUCCESS;
        }
    }
}
