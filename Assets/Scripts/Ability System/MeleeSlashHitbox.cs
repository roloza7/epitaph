using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSlashHitbox : MonoBehaviour
{
    public GameObject parent;
    public float damage;

    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            Enemy enemy = other.GetComponent<Enemy>();
            parent.GetComponent<Entity>().DealDamage(enemy, damage);

            var kb = other.GetComponent<Knockback>();
            kb?.KnockbackEntity(parent.GetComponent<Entity>().gameObject);
        }
        
        // Debug.Log($"Hit {other.gameObject.name}");
    }
}
