using UnityEngine;


public class HotBar {

    private SlotHolder<AbilityWrapper> slots;
    private Slot<AbilityWrapper> dashSlot;

    public HotBar(GameObject root, AbilityWrapper dash) {
        slots = new SlotHolder<AbilityWrapper>(root, 1);
        dashSlot = new Slot<AbilityWrapper>(root.transform.GetChild(0).gameObject, dash);
    }

    

}