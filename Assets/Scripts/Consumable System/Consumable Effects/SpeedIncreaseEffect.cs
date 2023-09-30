using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ConsumableModifiers/SpeedIncreaseEffect")]
public class SpeedIncreaseEffect : ConsumableModifier
{
    [SerializeField]
    public float baseMoveSpeedMultiplier = 1.5f;
    private float moveSpeedMultiplier;
    private SpeedIncrease modifier;
    
        
    public override void Init() {
        moveSpeedMultiplier = baseMoveSpeedMultiplier;
        modifier = new SpeedIncrease(moveSpeedMultiplier);
        basedOnTickRate = false;
    }

    public override void Activate(Entity player) {
        effectActive = true;
        ModifiableStat speed = player.EntityStats.GetStat(StatEnum.WALKSPEED);
        speed.AddModifier(modifier);
    }

    public override void Deactivate(Entity player) {
        effectActive = false;
        ModifiableStat speed = player.EntityStats.GetStat(StatEnum.WALKSPEED);
        speed.RemoveModifier(modifier);
    }
}

