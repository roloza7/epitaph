using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField]
    protected int cost; // cost of the item

    [SerializeField]
    protected string itemName; // name of the item

    public int getCost()
    {
        return this.cost;
    }

    public virtual void activate(Player player)
    {
        // do something
    }

}
