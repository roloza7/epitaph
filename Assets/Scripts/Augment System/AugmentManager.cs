using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentManager : MonoBehaviour
{

    [SerializeField] private bool DEBUG = false;

    [SerializeField]
    private Entity current;

    private Entity target;

    private float damageTaken = 0;

    private float damageDealt = 0;

    // active augments should be handled in the backend, no need to expose

    // Augments that are procced on hit
    private List<OnHitAugment> onHitAugments = new List<OnHitAugment>();

    // Augments that proc on player-related event
    private List<ListenerAugment> listenerAugments = new List<ListenerAugment>();

    // Augments that are continuous from run start to run end
    private List<StaticAugment> staticAugments = new List<StaticAugment>();
    public void setCurrent(Entity current)
    {
        this.current = current;
    }

    #region Run Event Triggers

    /*
    * Unused
    * This should be called right before player gets control on a run room i guess
    */
    public void onRunStart() {
        initializeStaticAugments();
        startCoroutines();
    }

    /*
    * Also unused
    * Called right after player loses control on a run room
    */
    public void onRunEnd() {
        removeStaticAugments();
        stopAllCoroutines();
        //clearAugments(); //disabled for the sake of debugging, should exist once we get rid of the debug script
    }

    #endregion

    #region Static Augments

    private void initializeStaticAugments() {
        if (!this.current) return;

        foreach (StaticAugment augment in staticAugments) {
            augment.applyAugment(this.current);
        }
    }

    private void removeStaticAugments() {
        if (!this.current) return;

        foreach (StaticAugment augment in staticAugments) {
            augment.removeAugment();
        }
    }

    #endregion

    #region Listener Augments

    public void startCoroutines()
    {
        foreach (ListenerAugment augment in listenerAugments)
        {
            augment.setCoroutineStarted(false);
            if (!augment.getCoroutineStarted())
            {
                augment.setCoroutineStarted(true);
                StartCoroutine(augment.passiveBehavior(current));
            }
        }
    }

    public void stopAllCoroutines() {
        StopAllCoroutines();
    }

    #endregion

    #region On-Hit Augments
    /*
    *   We have a bit of an issue here
    *   Apparently static and listener augments should be able to alter damage across the board
    *   There's probably something we can do here to make that happen
    *   Like half of the proposed passives need this to happen :(
    */

    // called whenever the player takes damage
    public void updateDamageTaken(float damage)
    {
        this.damageTaken = damage;
        takeAugmentedDamage();
    }

    // called whenever the player deals damage
    public void updateDamageDealt(Entity target, float damage, HashSet<AbilityTag> tags)
    {
        this.target = target;
        this.damageDealt = damage;
        dealAugmentedDamage(tags);
    }

    // this applies the augment AFTER we already took damage
    public void takeAugmentedDamage()
    {
        foreach (OnHitAugment augment in onHitAugments)
        {
            current.TakeDamageAugmented(augment.applyAugmentDamageTaken(damageTaken, current, target));
        }

        foreach (StaticAugment augment in staticAugments)
        {
            current.TakeDamageAugmented(augment.applyAugmentDamageTaken(damageTaken, current, target));
        }

        foreach (ListenerAugment augment in listenerAugments)
        {
            current.TakeDamageAugmented(augment.applyAugmentDamageTaken(damageTaken, current, target));
        }
    }

    // applies the augment AFTER we already deal damage
    public void dealAugmentedDamage(HashSet<AbilityTag> tags)
    {
        foreach(OnHitAugment augment in onHitAugments)
        {
            current.DealDamageAugmented(target, augment.applyAugmentDamageDealt(damageDealt, current, target, tags));
        }

        foreach (StaticAugment augment in staticAugments)
        {
            current.DealDamageAugmented(target, augment.applyAugmentDamageDealt(damageTaken, current, target, tags));
        }

        foreach (ListenerAugment augment in listenerAugments)
        {
            current.DealDamageAugmented(target, augment.applyAugmentDamageDealt(damageTaken, current, target, tags));
        }
    }

    
    #endregion

    #region General Utils
    public void addAugment(Augment augment) {
        if (DEBUG) Debug.Log("Adding augment");
        if (DEBUG) Debug.Log(augment.GetType());
        if (augment is OnHitAugment) {
            if (DEBUG) Debug.Log("Adding OnHitAugment");
            onHitAugments.Add((OnHitAugment)augment);
        }
        if (augment is ListenerAugment) {
            if (DEBUG) Debug.Log("Adding ListenerAugment");
            listenerAugments.Add((ListenerAugment)augment);
        }
        if (augment is StaticAugment) {
            if (DEBUG) Debug.Log("Adding StaticAugment");
            staticAugments.Add((StaticAugment)augment);
        }
    }

    public void clearAugments() {
        if (DEBUG) Debug.Log("[Augment Manager] Clearing all augments");
        onHitAugments.Clear();
        listenerAugments.Clear();
        staticAugments.Clear();

    }

    #endregion

}
