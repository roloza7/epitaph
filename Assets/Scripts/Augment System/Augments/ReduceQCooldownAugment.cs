using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reduce Q Cooldown", menuName = "Augments/Reduce Q Cooldown")]
public class ReduceQCooldownAugment : StaticAugment
{

    private Entity recipient;

    private SlotClass targetSlot;

    private float originalCooldown;

    [SerializeField]
    private float cooldownReduction = 0.99f;

    public override void applyAugment(Entity entity) {
        recipient = entity;
        targetSlot = entity.transform.gameObject.GetComponent<AbilityInventoryManager>().HotBarAbilities[1];
        if (targetSlot.isClear()) return;

        originalCooldown = targetSlot.GetAbility().getActiveAbility().cooldownTime;
        targetSlot.GetAbility().getActiveAbility().cooldownTime = originalCooldown * (1 - cooldownReduction);
        Debug.Log(originalCooldown);
        Debug.Log(targetSlot.GetAbility().getActiveAbility().cooldownTime);
    }

    public override void removeAugment() {
        if (targetSlot.isClear()) return;
        targetSlot.GetAbility().getActiveAbility().cooldownTime = originalCooldown;
    }

}
