using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : EnemyController
{
    private GameObject player;
    private Rigidbody2D rb;
    private Enemy enemy;
    private EntityStats stats;
    private float timer = 0;
    
    // Uncomment this to add in status effects when the projectile hits the player
    //[SerializeField] List<StatusEffect> _statusEffects;

    public float force;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        Vector3 rotation = transform.position;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot+60);
        enemy = gameObject.GetComponent<Enemy>();
        stats = enemy.EntityStats;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 12){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            Destroy(gameObject);
            player = other.gameObject;
            enemy.DealDamage(player.GetComponent<Player>(), stats.GetStatValue(StatEnum.ATTACK));
            // Uncomment this to add in status effects when the projectile hits the player
            //var statusEffectManager = player.GetComponent<StatusEffectManager>();
            //    statusEffectManager?.ApplyEffects(_statusEffects);
        }
    }
}
