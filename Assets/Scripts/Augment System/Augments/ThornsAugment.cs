using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsAugment : OnHitAugment
{
    [SerializeField]
    private float damageFactor = 0.5f; // assuming that you deal damage by a factor of what you take

    private float damageTaken;


    public override void firstActivation()
    {
        Debug.Log("Thorns activated.");
    }

    public override float applyAugmentDamageDealt(float damageDealt, Entity current, Entity target, HashSet<AbilityTag> tags)
    {
        return damageTaken;
    }

    public override float applyAugmentDamageTaken(float damageTaken, Entity current, Entity target)
    {
        this.damageTaken = damageTaken;
        return 0;
    }

}
