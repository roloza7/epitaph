using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityWrapper : ScriptableObject
{
    [SerializeField]
    private Ability activeAbility;

    [SerializeField]
    private Augment passiveAbility;

    public Ability getActiveAbility() { 
        return activeAbility;
    }

    public Augment getPassiveAbility() {
        return passiveAbility;
    }
}
