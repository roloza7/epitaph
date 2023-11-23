using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Life Steal Augment", menuName = "Augments/Life Steal")]
public class LifeStealAugment : Augment
{
    [SerializeField]
    private float stealFactor = 0.5f; // assuming that you gain health by a factor of the damage you deal

    private float damageDealt;

    public override float _ApplyAugmentDamageDealt(float damageDealt, Entity current, Entity target, HashSet<AbilityTag> tags)
    {
        this.damageDealt = damageDealt;
        return 0;
    }

    // note that this is negative because we want to heal
    public override float _ApplyAugmentDamageTaken(float damageTaken, Entity current, Entity target)
    {
        return -stealFactor * damageDealt;
    }
}
