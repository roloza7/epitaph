using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentManager : MonoBehaviour
{
    [SerializeField]
    private Entity current;

    private Entity target;

    private float damageTaken = 0;

    private float damageDealt = 0;

    // active augments should be handled in the backend, no need to expose
    private List<Augment> augments = new List<Augment>();

    // called whenever the player takes damage
    public void updateDamageTaken(float damage)
    {
        this.damageTaken = damage;
        // Debug.Log(current.name + ": " + current.HealthVal); // just for testing
        takeAugmentedDamage();
    }

    // called whenever the player deals damage
    public void updateDamageDealt(Entity target, float damage)
    {
        this.target = target;
        this.damageDealt = damage;
        dealAugmentedDamage();
    }

    // this applies the augment AFTER we already took damage
    public void takeAugmentedDamage()
    {
        foreach (Augment augment in augments)
        {
            current.TakeDamageAugmented(augment.applyAugmentDamageTaken(damageTaken, current, target));
        }
    }

    // applies the augment AFTER we already deal damage
    public void dealAugmentedDamage()
    {
        foreach(Augment augment in augments)
        {
            current.DealDamageAugmented(target, augment.applyAugmentDamageDealt(damageDealt, current, target));
        }
    }

    public void setCurrent(Entity current)
    {
        this.current = current;
    }

    // add active augments
    public void addAugment(Augment augment)
    {
        augments.Add(augment);
    }

    // deactive augments
    public void removeAugment(Augment augment)
    {
        augments.Remove(augment);
    }

}
