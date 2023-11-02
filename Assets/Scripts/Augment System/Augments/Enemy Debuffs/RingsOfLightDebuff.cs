using UnityEngine;

[CreateAssetMenu]
public class RingsOfLightDebuff : OnHitAugment
{
    [SerializeField]
    private float damage;

    public override void OnApply(Entity parent)
    {
    }
    
    public void ApplyProcVFX() {
    }

    public override void OnExpire(Entity parent)
    {
       if (!parent) return;
    }

    public override float applyAugmentDamageTaken(float damageDealt, Entity current, Entity target)
    {
        Debug.Log("Samsara damage spent");

        AugmentManager targetAugmentManager = current.GetComponent<AugmentManager>();
        if (!targetAugmentManager) return 0;

        targetAugmentManager.MarkRemoveAugment(this);
        return damage;
    }
}
