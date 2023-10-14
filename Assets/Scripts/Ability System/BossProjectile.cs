using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : EnemyProjectile
{
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public Rigidbody2D GetRB() { return rb; }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            var entity = other.GetComponent<Entity>();
            entity.GetComponent<Entity>().DealDamage(entity, damage);
            // Uncomment this to add in status effects when the projectile hits the player
            //var statusEffectManager = player.GetComponent<StatusEffectManager>();
            //    statusEffectManager?.ApplyEffects(_statusEffects);
            Destroy(this.gameObject);
        }
    }
}
