using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoonController : EnemyController
{
    [SerializeField] private float damageInterval;
    private float timer;
    private GameObject player;

    protected override void Start() {
        base.Start();
        timer = 0f;
    }

    protected override void Update() {
        if (this.isColliding) {
            timer += Time.deltaTime;
            if (timer > damageInterval) {
                enemy.DealDamage(player.GetComponent<Player>(), stats.GetStatValue(StatEnum.ATTACK));
                timer = 0f;
            }
        } else {
            timer = 0f;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D other) {
        base.OnCollisionEnter2D(other);
        if (other.gameObject.tag == "Player") {
            player = other.gameObject;
            enemy.DealDamage(player.GetComponent<Player>(), stats.GetStatValue(StatEnum.ATTACK));
        }
    }
}
