using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTArchitecture;

public class LichBT : BehaviorTree
{
    [SerializeField]
    private List<GameObject> shieldCrystals;

     protected override Node SetupTree() {
        GameObject target = GameObject.FindWithTag("Player");
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        Node root = new SelectorNode(new List<Node>{
            new CrystalDaemon(new List<Node>{ new BossMeteorNode() }),
            new LowHealthDaemon(),
            new NoShieldDaemon(),
            new SequenceNode(new List<Node>{
                new BossLongRangeShootNode(),
                new BossSpawnEnemyNode()
            })
        });


        List<Crystal> crystals = new List<Crystal>(this.transform.parent.gameObject.GetComponentsInChildren<Crystal>());

        root.SetData("gameobject", this.gameObject);
        root.SetData("crystals", crystals);
        root.SetData("entity", GetComponent<Entity>());
        root.SetData("agent", agent);
        root.SetData("controller", GetComponent<LichController>());
        root.SetData("player", GameObject.FindGameObjectsWithTag("Player")[0]); //KINDA LAME BUT MAYBE FIX LATER

        return root;
    }
}
