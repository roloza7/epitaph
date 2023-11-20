using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedGoonController : EnemyController
{
    private GameObject target;
    public EnemyProjectile projectile;
    private float timer;

    public float distanceAway;

    public float unloadSpeed;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player");
    }

    protected override void Update() {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist < distanceAway)
        {
            agent.velocity = Vector2.zero;
            timer += Time.deltaTime;
            if (timer > unloadSpeed)
            {
                timer = 0f;
                Shoot();
            }
        }
    }

    void Shoot(){
        Projectile proj = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
        proj.parent = gameObject;

    }
}
