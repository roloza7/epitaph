using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityWrapper : ScriptableObject, ISlotItem<AbilityWrapper>
{

    // Interface Methods
    public AbilityWrapper Item { get { return this; } }
    public Sprite ASprite { get { return activeAbility.aSprite; } }

    // End Interface Methods

    [SerializeField]
    private string wrapperName;
    public string WrapperName {
        get {
            return wrapperName;
        }
    }
    [SerializeField]
    private Ability activeAbility;

    public Ability ActiveAbility {
        get {
            return activeAbility;
        }
    }


    [SerializeField]
    private Augment passiveAbility;

    public Augment PassiveAbility {
        get {
            return passiveAbility;
        }
    }

    [SerializeField]
    private string description;
    public string Description {
        get {
            return description;
        }
    }

    public Ability getActiveAbility() { 
        return activeAbility;
    }

    public Augment getPassiveAbility() {
        return passiveAbility;
    }
    public string GetWrappedDescription() {
        return description + "<br><b>Active</b>: " + activeAbility.aDescription + "<br><b>Passive</b>: " + passiveAbility.aDescription;
    }
}
