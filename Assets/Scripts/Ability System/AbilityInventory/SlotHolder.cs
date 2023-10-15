using System.Collections.Generic;
using UnityEngine;

public class SlotHolder<T> where T : class, ISlotItem<T> {

    // Formatting properties
    [SerializeField]
    private int width;
    public readonly int size;

    // Internal properties
    private List<Slot<T>> slots; 

    public SlotHolder() {
        slots = new List<Slot<T>>();
    }



    public SlotHolder (GameObject root) : this() {
        int childCount = root.transform.childCount;
        slots.Capacity = childCount;
        for (int i = 0; i < childCount; i++) {
            slots[i] = new Slot<T>(root.transform.GetChild(i).gameObject);
        }
    }

}