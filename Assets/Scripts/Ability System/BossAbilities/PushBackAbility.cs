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
        Vector3 displacement = new Vector3 (parent.transform.position.x, parent.transform.position.y - 0.5f, parent.transform.position.z);
        PushBackIndicator pb = Instantiate(pushBack, displacement, Quaternion.identity).GetComponent<PushBackIndicator>();
        pb.parent = parent;
        Destroy(this.gameObject);
    }
}
