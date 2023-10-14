using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAbility : MonoBehaviour
{
    public string aName = "New Ability";
    public Sprite aSprite;
    public AudioClip aSound;
    public float activeTime = 1f;
    protected float currentActiveTime;

    public virtual void AbilityBehavior(GameObject caster) { 
        
    }

    public BossAbility GetAbility()
    {
        return this;
    }
}


