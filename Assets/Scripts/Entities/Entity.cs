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
    private Health _health;
    public float HealthVal => _health.health;
    public Health Health => _health;

    private StatusEffectManager _statusEffectManager;
    public StatusEffectManager StatusEffectManager => _statusEffectManager;

    [SerializeField] private float knockbackDelay;
    [SerializeField] private float knockbackForce;

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
        Debug.Log("dead");
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
        target.TakeDamage(dmgAmt);
        _augmentManager.updateDamageDealt(target, dmgAmt);
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


    public void Knockback(GameObject applier) {
        Debug.Log("KNOCK");
        StopAllCoroutines();
        Vector2 direction = (transform.position - applier.transform.position).normalized;
        body.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(ResetKnockBack());
    }

    private IEnumerator ResetKnockBack() {
        yield return new WaitForSeconds(knockbackDelay);
        body.velocity = Vector3.zero;
    }
}
