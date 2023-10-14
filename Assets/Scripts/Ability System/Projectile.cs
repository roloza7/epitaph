using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    protected Rigidbody2D rb;
    protected Vector3 direction;
    public float force;
    [SerializeField] protected float damage;
    [SerializeField] protected List<StatusEffect> _statusEffects;
    [SerializeField] protected float projectileTimer;
    public GameObject parent;

    // increase sprite size
    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (projectileTimer > 0)
        {
            projectileTimer -= Time.deltaTime;
            return;
        }
        Destroy(this.gameObject);
    }

    

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        var entity = other.GetComponent<Entity>();
        if (entity != null) {
            parent.GetComponent<Entity>().DealDamage(entity, damage);
        }
        var statusEffectManager = other.GetComponent<StatusEffectManager>();
            statusEffectManager?.ApplyEffects(_statusEffects);
        
        Debug.Log($"Hit {other.gameObject.name}");

        Destroy(this.gameObject);
    }

    public bool IsInsideTerrain() {
        LayerMask mask = LayerMask.GetMask("Terrain");
        Vector2 destination = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] hits = Physics2D.OverlapPointAll(destination, mask);
        if (hits.Length > 0) {
            foreach (Collider2D hit in hits) {
                Debug.Log(hit.gameObject.name);
            }
            return true;
        }
        return false;
    }
}
