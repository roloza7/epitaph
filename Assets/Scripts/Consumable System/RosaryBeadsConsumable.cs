using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the duration, cooldown, and activation of all consumable effects.
// Not yet implemented: Cooldown indicator on hotbar, # of charges indicator on hotbar
[CreateAssetMenu]
public class RosaryBeadsConsumable : ScriptableObject
{
    [SerializeField]
    public string cName = "Rosary Beads";
    [SerializeField]
    public string cDescription = "Description here";
    [SerializeField]
    public Sprite cSprite;
    [SerializeField]
    public AudioClip cSound;
    [SerializeField]
    public int startingCharges = 3;
    [SerializeField]
    public float baseEffectDuration = 5.0f;
    [SerializeField]
    public float baseCooldownTime = 1f;
    [SerializeField]
    public List<ConsumableModifier> startingModifiers;

    private bool consumablePressed;
    private CooldownState state;
    private float currentActiveTime;
    private float currentCooldownTime;
    private bool consumableEffectsActive;
    private HashSet<ConsumableModifier> consumableModifiers;

    // Upgrades to the consumable will affect these fields
    public int currentCharges;
    private float effectDuration;
    public float cooldownTime;

    public void Init() {
        currentCharges = startingCharges;
        effectDuration = baseEffectDuration;
        cooldownTime = baseCooldownTime;
        consumableEffectsActive = false;
        state = CooldownState.ready;
        consumableModifiers = new HashSet<ConsumableModifier>();
        foreach(ConsumableModifier modifier in startingModifiers) {
            consumableModifiers.Add(modifier);
            modifier.Init();
        }
    }

    public void Activate(GameObject parent) {
        consumableEffectsActive = true;
    }

    public void Deactivate(GameObject parent) {
        consumableEffectsActive = false;
    }

    public void ConsumableBehavior(Entity player) {
        if (consumableEffectsActive) {
            Debug.Log("consumable active");
            foreach(ConsumableModifier modifier in consumableModifiers) {
                if (modifier.basedOnTickRate) {
                    modifier.Activate(player);
                } else {
                    if (!modifier.effectActive) {
                        modifier.Activate(player);
                    }
                }
            }
        } else {
            foreach(ConsumableModifier modifier in consumableModifiers) {
                if (!modifier.basedOnTickRate) {
                    modifier.Deactivate(player);
                }
            }
        }
    }

    // Handles consumable cooldown system
    public void ConsumableCooldownHandler(GameObject parent) {
        switch (state) {
            case CooldownState.ready:
                if (consumablePressed && hasCharges()) {
                    Activate(parent);
                    state = CooldownState.active;
                    currentActiveTime = effectDuration;
                    // fillAmount = 1;
                    consumablePressed = false;
                    decreaseCharges();
                }
            break;
            case CooldownState.active:
                if (currentActiveTime > 0) {
                    currentActiveTime -= Time.deltaTime;
                } else {
                    state = CooldownState.cooldown;
                    currentCooldownTime = cooldownTime;
                    Deactivate(parent);
                }
            break;
            case CooldownState.cooldown:
                if (currentCooldownTime > 0) {
                    currentCooldownTime -= Time.deltaTime;
                    // fillAmount -= 1/cooldownTime * Time.deltaTime;
                } else {
                    state = CooldownState.ready;
                    // fillAmount = 1;
                }
            break;
        }
    }

    public void SetConsumablePressed(bool pressed) {
        consumablePressed = pressed;
    }

    // Intended for use in the shop when buying upgrades for the consumable
    public bool AddConsumableModifier(ConsumableModifier modifier) {
        modifier.Init();
        return consumableModifiers.Add(modifier);
    }

    public bool hasCharges() {
        return currentCharges > 0;
    }

    // Intended for use in shop when buying more charges
    public void addCharges(int chargeAmount) {
        currentCharges += chargeAmount;
    }

    public void decreaseCharges() {
        currentCharges--;
    }

    public List<string> GetConsumableDescriptions() {
        List<string> descriptions = new List<string>();
        foreach (ConsumableModifier mod in consumableModifiers) {
            descriptions.Add(mod.cmDesc);
        }
        return descriptions;
    }

}

public enum CooldownState {
    ready,
    active,
    cooldown
}

