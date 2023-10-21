using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CircleDamageAbility : ProjectileAbility
{
    [SerializeField]
    private float aoeRadius = 1;

    [SerializeField]
    private float damage = 5;

    public override void Init() {
        //bulletTransform = bulletSpawner.transform;
        firing = false;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public override void AbilityBehavior(GameObject parent) {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //Projectile proj = Instantiate(circle, mousePos, Quaternion.identity);
        //proj.parent = parent;
        if (firing) {
            Debug.Log("circlefired");
            Projectile proj = Instantiate(projectile, mousePos, Quaternion.identity);
            //proj.parent = parent;
            firing = false;
            
            LayerMask mask = LayerMask.GetMask("Enemy");

            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(mask);
            List<Collider2D> enemiesHit = new List<Collider2D>();
            int i = Physics2D.OverlapCircle(proj.transform.position, aoeRadius, filter, enemiesHit); 

            Entity player = parent.GetComponent<Entity>();

            foreach(Collider2D collider in enemiesHit) {
                Enemy enemy = collider.GetComponent<Enemy>();
                player.DealDamage(enemy, damage);
            }
        } 
    }
        

     public override void AbilityCooldownHandler(GameObject parent) {
        switch (state) 
        {
            case AbilityState.ready:
                if (abilityPressed) {
                    Activate(parent);
                    state = AbilityState.active;
                    currentActiveTime = activeTime;
                    fillAmount = 1;
                }
            break;
            case AbilityState.active:
                if (currentActiveTime > 0) {
                    currentActiveTime -= Time.deltaTime;
                } else {
                    state = AbilityState.cooldown;
                    currentCooldownTime = cooldownTime;
                    Deactivate(parent);
                }
            break;
            case AbilityState.cooldown:
                if (currentCooldownTime > 0) {
                    currentCooldownTime -= Time.deltaTime;
                    fillAmount -= 1/cooldownTime * Time.deltaTime;
                } else {
                    state = AbilityState.ready;
                    fillAmount = 1;

                }
            break;
        }
    }

    

}
