using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatusEffectManager))]
[RequireComponent(typeof(AugmentManager))]
public class Entity : MonoBehaviour 
{
    [SerializeField] private EntityStats _entityStats;
    public EntityStats EntityStats => _entityStats;
    [SerializeField] private float intialHealth;
    protected Health _health;
    public float HealthVal => _health.health;
    public Health Health => _health;

    private StatusEffectManager _statusEffectManager;
    public StatusEffectManager StatusEffectManager => _statusEffectManager;

    private Rigidbody2D body;

    private bool _isDead;

    // need a reference to this to adjust damage dealt and taken
    [SerializeField]
    private AugmentManager _augmentManager;

    private void Awake() {
        _health = new(this, intialHealth);
        _statusEffectManager = gameObject.GetComponent<StatusEffectManager>();
        _statusEffectManager.entity = this;
        _augmentManager = gameObject.GetComponent<AugmentManager>();
        _augmentManager.setCurrent(this);
    }

    protected virtual void Start()
    {
        //override in child classes
        body = this.gameObject.GetComponent<Rigidbody2D>();
    }

    public virtual void Die() {
        if(_isDead) return;
        //override in child classes
        //Debug.Log("dead");
    }

    // relaying data to augment manager
    public void TakeDamage(float amount)
    {
        Health.TakeDamage(amount);
        _augmentManager.updateDamageTaken(amount);
    }

    // relaying data to augment manager
    public void DealDamage(Entity target, float dmgAmt)
    {
        DealDamage(target, dmgAmt, new HashSet<AbilityTag>());
    }

    // relaying data to augment manager with tag
    public void DealDamage(Entity target, float dmgAmt, HashSet<AbilityTag> tags)
    {
        target.TakeDamage(dmgAmt);
        _augmentManager.updateDamageDealt(target, dmgAmt, tags);
    }

    // handle augmented damage taken after initial
    public void TakeDamageAugmented(float amount)
    {
        Health.TakeDamage(amount);
    }

    // handle augmented damage dealt after initial
    public void DealDamageAugmented(Entity target, float dmgAmt)
    {
        target.TakeDamage(dmgAmt);
    }
}
    