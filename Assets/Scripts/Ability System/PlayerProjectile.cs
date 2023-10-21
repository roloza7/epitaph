using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    // Start is called before the first frame update
    private Vector3 mousePos;
    private Camera mainCam;
    protected override void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }
    protected override void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            var statusEffectManager = other.GetComponent<StatusEffectManager>();
                statusEffectManager?.ApplyEffects(_statusEffects);
            var entity = other.GetComponent<Entity>();
            if (entity != null) {
                parent.GetComponent<Entity>().DealDamage(entity, damage);
            }
            Debug.Log($"Hit {other.gameObject.name}");

        }
        Destroy(this.gameObject);
    }

}
