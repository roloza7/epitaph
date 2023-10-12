using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableModifier : ScriptableObject
{
    public string cmName = "New Consumable Modifier";
    [SerializeField]
    public float tickRate = 0.5f;
    [SerializeField] public string cmDesc = "New Description";
    public bool basedOnTickRate;
    // For use with effects like speed increase, which are only activated once
    // and last for the duration of the consumable.
    public bool effectActive;

    public virtual void Init() {}

    // Activate the consumable effect
    public virtual void Activate(Entity player) {}

    // Deactivate the consumable effect
    public virtual void Deactivate(Entity player) {}

}


