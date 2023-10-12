using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTArchitecture
{
    public class BossLongRangeShootNode : Node
    {
        public BossLongRangeShootNode() { }

        public override NodeState Evaluate()
        {

            return NodeState.SUCCESS;
        }
    }
}
