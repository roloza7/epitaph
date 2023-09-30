using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DamageAuraAugment : ListenerAugment
{
    [SerializeField]
    private float radius;
    [SerializeField]
    private float interval;
    [SerializeField]
    private float damage;

    public override IEnumerator passiveBehavior(Entity player)
    {
        Debug.Log(player);
        while (true)
        {
            LayerMask mask = LayerMask.GetMask("Enemy");

            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(mask);
            List<Collider2D> enemiesHit = new List<Collider2D>();
            int i = Physics2D.OverlapCircle(player.transform.position, radius, filter, enemiesHit);

            foreach (Collider2D collider in enemiesHit)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                player.DealDamage(enemy, damage);
            }
            yield return new WaitForSeconds(interval);
        }
    }


}
