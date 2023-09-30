using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class DashAbility : Ability
{
    public float moveSpeedMultiplier;

    private SpeedIncrease modifier;
    
    public override void Init() {
        modifier = new SpeedIncrease(moveSpeedMultiplier);
    }

    public override void Activate(GameObject parent)
    {
        Debug.Log("dash start");
        ModifiableStat speed = parent.GetComponent<Player>().EntityStats.GetStat(StatEnum.WALKSPEED);
        speed.AddModifier(modifier);
        parent.GetComponent<PlayerController>().CanChangeDirection = false;
    }

    public override void Deactivate(GameObject parent)
    {
        Debug.Log("dash end");
        ModifiableStat speed = parent.GetComponent<Player>().EntityStats.GetStat(StatEnum.WALKSPEED);
        speed.RemoveModifier(modifier);
        parent.GetComponent<PlayerController>().CanChangeDirection = true;
    }

    public override void AbilityCooldownHandler(GameObject parent) {
        switch (state) 
        {
            case AbilityState.ready:
                if (abilityPressed) {
                    Activate(parent);
                    state = AbilityState.active;
                    currentActiveTime = activeTime;
                    fillAmount = 1;
                }
            break;
            case AbilityState.active:
                if (currentActiveTime > 0) {
                    currentActiveTime -= Time.deltaTime;
                } else {
                    state = AbilityState.cooldown;
                    currentCooldownTime = cooldownTime;
                    Deactivate(parent);
                }
            break;
            case AbilityState.cooldown:
                if (currentCooldownTime > 0) {
                    currentCooldownTime -= Time.deltaTime;
                    fillAmount -= 1/cooldownTime * Time.deltaTime;
                } else {
                    state = AbilityState.ready;
                    fillAmount = 1;

                }
            break;
        }
    }

}