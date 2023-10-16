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

    // public void RefreshHotBar() 
    // {
    //     // Refresh the dash ability on the hotbar
    //     hotbarAbilities[0].GetAbility().getActiveAbility().Init();
    //     hotbarAbilities[0].GetAbility().getActiveAbility().SetState(AbilityState.ready);
    //     hotbarSlots[0].transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
    //     hotbarSlots[0].GetComponent<TooltipFormatter>().Ability = dashAbility;


    //     // Start at 1 to account for the dash ability taking up a slot
    //     for (int i = 1; i < hotbarSlots.Length; i++) {
    //         try {
    //             //slots[i].transform.GetChild(0).GetComponent<Image>().sprite = abilities[i].GetAbility().aSprite;
    //             hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = slots.Items[(i - 1) + NUMBER_OF_ABILITIES * 2].getActiveAbility().aSprite;
    //             hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;

    //             // CHANGE THIS IMMEDIATELY
    //             hotbarAbilities[i] = new SlotClass(slots.Items[(i - 1) + NUMBER_OF_ABILITIES * 2]);
    //             hotbarSlots[i].GetComponent<TooltipFormatter>().Ability = hotbarAbilities[i].GetAbility();

    //             if (hotbarAbilities[i].GetAbility() != null) {
    //                 hotbarAbilities[i].GetAbility().getActiveAbility().Init();
    //                 hotbarAbilities[i].GetAbility().getActiveAbility().SetState(AbilityState.ready);
    //                 hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
    //                 hotbarSlots[i].GetComponent<TooltipFormatter>().Ability = hotbarAbilities[i].GetAbility();
    //             }

    //         } catch {
    //             hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
    //             hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
    //             hotbarSlots[i].GetComponent<TooltipFormatter>().Ability = null;
    //         }
    //     }        
    // }

}