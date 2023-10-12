using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkDaggerAbility : ProjectileAbility
{
    //private Camera mainCam;
    //private Vector3 mousePos;
    //private bool firing;
    // public GameObject dagger;
    //private bool canFire;
    public float delay;
    private bool daggerThrown;
    private Projectile thrownDagger;
    
    [SerializeField]
    private float aoeRadius = 1;

    [SerializeField]
    private float damage = 5;
    public bool teleported;

    public bool destroyed;

    // public override void Activate(GameObject parent)
    // {
    //     firing = true;
    // }

    // public override void Deactivate(GameObject parent) 
    // {
    //     firing = false;
    // }

    public override void Init() {
        base.Init();
        firing = false;
        teleported = false;
        daggerThrown = false;
    }

    public override void AbilityBehavior(GameObject parent) {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        //Debug.Log("Blink Started");

        if (!canFire) {
            timer += Time.deltaTime;
            if (timer > delay) {
                canFire = true;
                timer = 0;
            }
        }

        if (firing && !daggerThrown && canFire) {
            Vector2 pos = parent.transform.GetChild(0).GetChild(0).transform.position;
            ThrowDagger(pos, parent);
        } else if (firing && daggerThrown && canFire) {
            GoToDagger(parent);
        }
    }

    public override void AbilityCooldownHandler(GameObject parent) {
        switch (state) 
        {
            case AbilityState.ready:
                if (abilityPressed) {
                    Activate(parent);
                    state = AbilityState.reactive;
                    fillAmount = 1;
                    abilityPressed = false;
                }
            break;
            case AbilityState.reactive:
                if (thrownDagger == null) {
                    destroyed = true;
                    firing = false;
                }
                if (abilityPressed || destroyed) {
                    Activate(parent);
                    state = AbilityState.active;
                }
            break;
            case AbilityState.active:
                if (thrownDagger == null) {
                    destroyed = true;
                    firing = false;
                }
                if (teleported || destroyed) {
                    currentCooldownTime = cooldownTime;
                    state = AbilityState.cooldown;
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

    #region DaggerMethods
    private void ThrowDagger(Vector2 pos, GameObject parent) {
        daggerThrown = true;
        canFire = false;
        teleported = false;
        thrownDagger = Instantiate(projectile, pos, Quaternion.identity);
        thrownDagger.parent = parent;
        firing = false;
        destroyed = false;
    }

    private void GoToDagger(GameObject parent) {
        daggerThrown = false;
        canFire = false;
        teleported = true;
        if (thrownDagger == null) {
            destroyed = true;
            firing = false;
            return;
        }
        parent.transform.position = thrownDagger.transform.position;

        Destroy(thrownDagger.gameObject);
        firing = false;

        // Damage enemies in a small area

        // Mask for enemies
        LayerMask mask = LayerMask.GetMask("Enemy");

        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(mask);
        List<Collider2D> enemiesHit = new List<Collider2D>();
        int i = Physics2D.OverlapCircle(parent.transform.position, aoeRadius, filter, enemiesHit); 

        Entity player = parent.GetComponent<Entity>();

        foreach(Collider2D collider in enemiesHit) {
            Enemy enemy = collider.GetComponent<Enemy>();
            player.DealDamage(enemy, damage);
        }

    }
    #endregion

}
