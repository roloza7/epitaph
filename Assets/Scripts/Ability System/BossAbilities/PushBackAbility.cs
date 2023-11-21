using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PushBackAbiliity : BossAbility
{
    [SerializeField]
    public float delay;
    public GameObject pushBack;

    public override void AbilityBehavior(GameObject parent)
    {
        print("push back");
        PushBackIndicator pb = Instantiate(pushBack, parent.transform.position, Quaternion.identity).GetComponent<PushBackIndicator>();
        pb.parent = parent;
        Destroy(this.gameObject);
    }
}
