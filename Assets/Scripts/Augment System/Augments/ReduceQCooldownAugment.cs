using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reduce Q Cooldown", menuName = "Augments/Reduce Q Cooldown")]
public class ReduceQCooldownAugment : StaticAugment
{

    private Entity recipient;

    private Slot<AbilityWrapper> targetSlot;

    private float originalCooldown;

    [SerializeField]
    private float cooldownReduction = 0.70f;

    public override void applyAugment(Entity entity) {
        recipient = entity;
        targetSlot = entity.transform.gameObject.GetComponent<AbilityInventoryManager>().hotbar.GetMutableAbilitySlot(0);
        if (targetSlot.IsClear()) return;

        originalCooldown = targetSlot.Item.ActiveAbility.cooldownTime;
        targetSlot.Item.ActiveAbility.cooldownTime = originalCooldown * (1 - cooldownReduction);
    }

    public override void removeAugment() {
        if (targetSlot.IsClear()) return;
        targetSlot.Item.ActiveAbility.cooldownTime = originalCooldown;
    }

}
