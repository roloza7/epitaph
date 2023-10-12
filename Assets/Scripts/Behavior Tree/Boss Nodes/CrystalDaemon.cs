using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//checks if Crystal [number] is being attacked

namespace BTArchitecture {
    public class CrystalDaemon : Node
    {
        int number = 0;
        public CrystalDaemon() : base() { }

        public CrystalDaemon(List<Node> children) : base(children) { }

        public override NodeState Evaluate() {
            List<Crystal> crystals = (List<Crystal>) this.GetData("crystals");
            bool damaged = crystals[number].WasDamaged();
            if (!damaged){
			    return NodeState.FAILURE;
            } else {
			    return children[0].Evaluate();
            }
        }
    }
}
