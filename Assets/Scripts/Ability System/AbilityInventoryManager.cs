using System;
using UnityEngine;

public class AbilityInventoryManager : MonoBehaviour
{

    [SerializeField] private bool DEBUG = false;

    private AbilityCursor cursor;

    //public List<Ability> abilities11 = new List<Ability>();

    [SerializeField] private AbilityWrapper[] startingAbilities;
    [SerializeField] private AbilityWrapper dashAbility;
    // private SlotClass[] abilities;

    // Fancy new classes for holding items
    public SlotHolder<AbilityWrapper> slots;

    public HotBar hotbar { get; private set; }
    

    // private GameObject[] slots;
    private AugmentManager augmentManager;


    private Slot<AbilityWrapper> originalSlot;
    bool isMovingItem;

    private bool managerActive;

    // Start is called before the first frame update
    private void Awake() {
        slots = new SlotHolder<AbilityWrapper>(GameObject.FindWithTag("Slots"));
        hotbar = new HotBar(GameObject.FindWithTag("HotBar"), dashAbility);
        cursor = GameObject.FindWithTag("Cursor").GetComponent<AbilityCursor>();
        managerActive = false;
    }


    private void Start()
    {
        // Add Starting Abilities
        for (int i = 0; i < startingAbilities.Length; i++) {
            Debug.Log("Acessing Index " + i + " to set starting abilities, array of length " + slots.Length);
            slots.Items[i] = startingAbilities[i];
            Debug.Log("[Sprite] " + slots[i].image.sprite);
        }

        // Add AugmentManager reference so we can update augments
        augmentManager = gameObject.GetComponent<AugmentManager>();
        
        hotbar.Refresh();    
    }

    private void Update() {
        // One less than hotbar slots length bc dash ability takes up a slot
        if (isMovingItem && !managerActive) {
            EndItemMove();
        }
        if (Input.GetMouseButtonDown(0) && managerActive) 
        {
            if (isMovingItem) {
                EndItemMove();
            } else {
                BeginItemMove();
            }
            hotbar.Refresh();
        }
    }

    #region Active / Passive Utils

    // TODO: Make this work with rework
    // public void RefreshEnabledAugments() {

    //     augmentManager.clearAugments();
    //     for (int i = 0; i < slots.Length - hotbarSlots.Length + 1; i++) {
    //         AbilityWrapper slot = slots.Items[i];
    //         if (slot != null) {
    //             if (slot.getPassiveAbility() == null) {
    //                 if (DEBUG) Debug.Log("[AbilityInventoryManager] Ability " + slot + " has no associated passive. This shouldn't happen");
    //                 continue;
    //             }
    //             augmentManager.addAugment(slot.getPassiveAbility());
    //         };
    //     }
    //     // Realistically we want this to be only applied to the AugmentManager attached to the player, so this is safe
        

    // }

    #endregion

    #region Inventory Utils

    /**
    *   Add
    *   Adds Ability to first available slot
    *   
    *   return: true if ability was successfully added, false if not 
    **/
    public bool Add(AbilityWrapper ability)
    {
        Slot<AbilityWrapper> slotToFill = null;

        foreach (Slot<AbilityWrapper> slot in slots) {
            if (slot.Item == null) slotToFill = slot;
            if (slot.Item == ability) {
                return false;
            }
        }

        slotToFill.Item = ability;
        return true;
    }

    #endregion Inventory Utils

    #region Drag And Drop
    private enum AbilitySource {
        inventory,
        hotbar
    }
    private Tuple<Slot<AbilityWrapper>, AbilitySource> GetClosestSlot() {
        foreach (Slot<AbilityWrapper> slot in slots) {
            if (Vector2.Distance(slot.gameObject.transform.position, Input.mousePosition) <= 32)
                return Tuple.Create(slot, AbilitySource.inventory);
        }

        foreach (Slot<AbilityWrapper> slot in hotbar.MutableAbilities) {
            if (Vector2.Distance(slot.gameObject.transform.position, Input.mousePosition) <= 32)
                return Tuple.Create(slot, AbilitySource.hotbar);;
        }

        return null;
    }

    private void BeginItemMove() {
        Tuple<Slot<AbilityWrapper>, AbilitySource> closestSlot = GetClosestSlot();
        if (closestSlot == null) {
            return;
        } 
        originalSlot = closestSlot.Item1;

        // Reset ability state
        if (closestSlot.Item2 == AbilitySource.hotbar)
            originalSlot.Item.ActiveAbility.Reset(gameObject);

        cursor.Ability = originalSlot.Item; 
        originalSlot.Item = null;
        isMovingItem = true;
        return;
    }

    private void EndItemMove() {
        Tuple<Slot<AbilityWrapper>, AbilitySource> closestSlot = GetClosestSlot();
        if (closestSlot == null) {
            // Moving failed, revert
            originalSlot.Item = cursor.Ability;
            cursor.Ability = null;
        } else {
            // Found a candidate slot
            Slot<AbilityWrapper> slotToFill = closestSlot.Item1;
            if (slotToFill.Item != null) {
                // Slot is filled, do some swapping
                AbilityWrapper temp = slotToFill.Item;
                slotToFill.Item = cursor.Ability;
                cursor.Ability = temp;
                // Don't null originalSlot, we might need it later
                return;
            } else {
                // We can safely null originalSlot
                originalSlot.Item = null;

                // Slot is not filled
                slotToFill.Item = cursor.Ability;
                cursor.Ability = null;
            }
        }
        isMovingItem = false;
        return;
    }
    #endregion Drag And Drop
    public void SetManagerActive(bool active) {
        managerActive = active;
    }
}
