using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingGoonController : EnemyController
{
    private GameObject target;

    public GameObject healingTerrain;
    private float timer;
    public float distanceAway;
    public float healCD;
    private void Awake()
    {
        target = GameObject.FindWithTag("Player");
    }

    protected override void Update() {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist < distanceAway && dist > fleeDist)
        {
            agent.velocity = Vector2.zero;
            timer += Time.deltaTime;
            if (timer > healCD)
            {
                timer = 0f;
                Vector3 spawnLoc = SweepArea();
                SpawnTerrain(spawnLoc);
            }
        }
    }

    Vector3 SweepArea() {
        float lowestHealth = 1.0f;
        Enemy lowestEnemy = gameObject.GetComponent<Enemy>();
        Collider2D[] hit = Physics2D.OverlapCircleAll(gameObject.transform.position, distanceAway, LayerMask.GetMask("Enemy"));
        foreach (Collider2D collider in hit) {
            Enemy enemy = collider.GetComponent<Enemy>();
            float healthPercent = enemy.Health.GetStatValue() / enemy.Health.intialValue;
            if (healthPercent < lowestHealth) {
                lowestHealth = healthPercent;
                lowestEnemy = enemy;
            }
        }
        return lowestEnemy.transform.position;
    }

    void SpawnTerrain(Vector3 pos) {
        Instantiate(healingTerrain, pos, Quaternion.identity);
    }
}
