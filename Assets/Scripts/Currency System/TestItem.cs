using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TestItem : Item
{
    public override void activate(Player player)
    {
        Debug.Log(this.itemName + " Activated!");
    }
}
