using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAugment : Augment
{

    private Entity recipient = null;

    public virtual void applyAugment(Entity entity) {
        recipient = entity;
    }

    public virtual void removeAugment() {
    }

}
