using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTArchitecture
{
    //Checks if there the shield isn't activated for the Lich
    public class NoShieldDaemon : Node
    {
        public NoShieldDaemon() : base() { }

        public NoShieldDaemon(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            LichController controller = (LichController)this.GetData("controller");
            if (controller.HasShield())
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
