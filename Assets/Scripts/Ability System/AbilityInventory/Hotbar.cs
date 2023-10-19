using UnityEngine;


public class HotBar {

    private SlotHolder<AbilityWrapper> slots;

    public SlotHolder<AbilityWrapper> MutableAbilities { get { return slots; } }
    private Slot<AbilityWrapper> dashSlot;

    public HotBar(GameObject root, AbilityWrapper dash) {
        slots = new SlotHolder<AbilityWrapper>(root, 1);
        dashSlot = new Slot<AbilityWrapper>(root.transform.GetChild(0).gameObject, dash);
    }

    public Slot<AbilityWrapper> GetDashAbilitySlot() => dashSlot;
    public Slot<AbilityWrapper> GetMutableAbilitySlot(int key) => slots[key];

    private static void RefreshAbility(Slot<AbilityWrapper> slot) { 
        slot.gameObject.GetComponent<TooltipFormatter>().Ability = slot.Item;
        if (slot.Item == null)
            return;

        slot.Item.ActiveAbility.Init();
        slot.Item.ActiveAbility.SetState(AbilityState.ready);
        slot.Item.ActiveAbility.fillAmount = 1;
        slot.SetFillAmount(1);
    }

    public void Refresh() {
        // Refresh Dash Ability
        RefreshAbility(dashSlot);

        foreach (Slot<AbilityWrapper> slot in slots) {
            if (slot.Item == null) continue;
            RefreshAbility(slot);
        }
    }

    public void SetupActiveBarListener(SlotHolder<AbilityWrapper> actives) {

        if (actives.Length != slots.Length)
            throw new System.Exception("[Hotbar.cs] Trying to setup listener between two SlotBars of different sizes, what?");
        
        for (int i = 0; i < actives.Length; i++) {
            Slot<AbilityWrapper> listener = slots[i];
            actives[i].callback = (Slot<AbilityWrapper> slot, AbilityWrapper _) => {
                if (listener.Item == slot.Item) return;
                listener.Item = slot.Item;
                RefreshAbility(listener);
            };
        }
    }

}