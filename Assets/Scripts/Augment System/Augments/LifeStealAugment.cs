using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LifeStealAugment : OnHitAugment
{
    [SerializeField]
    private float stealFactor = 0.5f; // assuming that you gain health by a factor of the damage you deal

    private float damageDealt;


    public override void firstActivation()
    {
        Debug.Log("LifeSteal activated.");
    }

    public override float applyAugmentDamageDealt(float damageDealt, Entity current, Entity target, HashSet<AbilityTag> tags)
    {
        this.damageDealt = damageDealt;
        return 0;
    }

    // note that this is negative because we want to heal
    public override float applyAugmentDamageTaken(float damageTaken, Entity current, Entity target)
    {
        return -stealFactor * damageDealt;
    }
}
