using Unity.VisualScripting;
using UnityEngine;


public class HotBar {

    private SlotHolder<AbilityWrapper> slots;

    public SlotHolder<AbilityWrapper> MutableAbilities { get { return slots; } }
    private Slot<AbilityWrapper> dashSlot;

    public HotBar(GameObject root, AbilityWrapper dash) {
        slots = new SlotHolder<AbilityWrapper>(root, 1);
        dashSlot = new Slot<AbilityWrapper>(root.transform.GetChild(0).gameObject, dash);
        Debug.Log("DashSlot: " + dashSlot);
    }

    public Slot<AbilityWrapper> GetDashAbilitySlot() => dashSlot;
    public Slot<AbilityWrapper> GetMutableAbilitySlot(int key) => slots[key];

    private static void _RefreshAbility(Slot<AbilityWrapper> slot) { 
        slot.gameObject.GetComponent<TooltipFormatter>().Ability = slot.Item;
        if (slot.Item == null)
            return;

        slot.Item.ActiveAbility.Init();
        slot.Item.ActiveAbility.SetState(AbilityState.ready);
        slot.SetFillAmount(0);
    }

    public void Refresh() {
        // Refresh Dash Ability
        _RefreshAbility(dashSlot);

        foreach (Slot<AbilityWrapper> slot in slots) {
            if (slot.Item == null) continue;
            _RefreshAbility(slot);
        }
    }

}