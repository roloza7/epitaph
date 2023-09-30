using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ConsumableModifiers/HealOverTimeEffect")]
public class HealOverTime : ConsumableModifier
{
    [SerializeField]
    public float baseHealAmountPerTick = 2.0f;
    private float healAmountPerTick;
    private float timer;

    public override void Init() {
        healAmountPerTick = baseHealAmountPerTick;
        basedOnTickRate = true;
    }

    public override void Activate(Entity player) {
        timer += Time.deltaTime;
        if (timer > tickRate) {
            player.Health.Heal(healAmountPerTick);
            timer = 0;
        }
    }

}


