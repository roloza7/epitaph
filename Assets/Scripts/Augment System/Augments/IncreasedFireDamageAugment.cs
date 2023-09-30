using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Increased Fire Damage Augment", menuName = "Augments/Increased Fire Damage")]
public class IncreasedFireDamageAugment : StaticAugment
{
    [SerializeField]
    private float damageMultiplier = 2f;
    public override float applyAugmentDamageDealt(float damageDealt, Entity current, Entity target, HashSet<AbilityTag> tags)
    {
        Debug.Log(tags);
        if (tags.Contains(AbilityTag.FIRE)) {
            Debug.Log("Fire Damage");
            return damageDealt * damageMultiplier;
        }
        return 0;
    }

}