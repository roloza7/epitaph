using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTArchitecture
{
    // Checks if the Lich is at low health
    public class LowHealthDaemon : Node
    {
        public LowHealthDaemon() : base() { }

        public LowHealthDaemon(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            Entity lich = (Entity)this.GetData("entity");
            if (lich.Health.health > 20)
            {
                return NodeState.FAILURE;
            }
            else
            {
                return children[0].Evaluate();
            }
        }
    }
}
