using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AbilityInventoryManager : MonoBehaviour
{

    [SerializeField] private bool DEBUG = false;

    private GameObject abilityCursor;
    private GameObject slotHolder;
    private GameObject hotbarSlotHolder;

    [SerializeField] private AbilityWrapper newAbility;
    [SerializeField] private AbilityWrapper abilityToDiscard;


    //public List<Ability> abilities11 = new List<Ability>();

    [SerializeField] private AbilityWrapper[] startingAbilities;
    [SerializeField] private AbilityWrapper dashAbility;
    // private SlotClass[] abilities;
    private SlotClass[] hotbarAbilities;

    // Getter so we can influence skill behavior using augments
    public SlotClass[] HotBarAbilities { get { return hotbarAbilities; }}

    // Fancy new classes for holding items
    public SlotHolder<AbilityWrapper> inventorySlots;

    // private GameObject[] slots;
    private GameObject[] hotbarSlots;
    private static int NUMBER_OF_ABILITIES;

    private AugmentManager augmentManager;


    private SlotClass movingSlot;
    private SlotClass tempSlot;
    private Slot<AbilityWrapper> originalSlot;
    bool isMovingItem;

    private bool managerActive;

    // Start is called before the first frame update
    private void Awake() {
        abilityCursor = GameObject.FindWithTag("Cursor");
        slotHolder = GameObject.FindWithTag("Slots");
        hotbarSlotHolder = GameObject.FindWithTag("HotBar");
        managerActive = false;
    }


    private void Start()
    {

        inventorySlots = new SlotHolder<AbilityWrapper>(slotHolder);

        // slots = new GameObject[slotHolder.transform.childCount];
        // abilities = new SlotClass[slots.Length];

        hotbarSlots = new GameObject[hotbarSlotHolder.transform.childCount];
        hotbarAbilities = new SlotClass[hotbarSlots.Length];

        for (int i = 0; i < hotbarSlots.Length; i++) {
            hotbarSlots[i] = hotbarSlotHolder.transform.GetChild(i).gameObject;
        }
        // One less than hotbar slots length bc dash ability takes up a slot
        NUMBER_OF_ABILITIES = hotbarSlots.Length - 1;

        // Add the dash ability to the hotbar slots
        hotbarAbilities[0] = new SlotClass(dashAbility);

        for (int i = 1; i < hotbarAbilities.Length; i++) {
            hotbarAbilities[i] = new SlotClass();
        }

        // Add Starting Abilities
        for (int i = 0; i < startingAbilities.Length; i++) {
            Debug.Log("Acessing Index " + i + " to set starting abilities, array of length " + inventorySlots.Length);
            inventorySlots.Items[i] = startingAbilities[i];
        }

        // for (int i = 0; i < startingAbilities.Length; i++) {
        //     abilities[i] = startingAbilities[i];
        // }

        // for (int i = 0; i < slotHolder.transform.childCount; i++) {
        //     slots[i] = slotHolder.transform.GetChild(i).gameObject;
        // }

        // Add the dash ability image to the hotbar
        hotbarSlots[0].transform.GetChild(0).GetComponent<Image>().sprite = dashAbility.getActiveAbility().aSprite;
        hotbarSlots[0].transform.GetChild(0).GetComponent<Image>().enabled = true;
        hotbarSlots[0].GetComponent<TooltipFormatter>().Ability = dashAbility;

        // Add AugmentManager reference so we can update augments
        augmentManager = gameObject.GetComponent<AugmentManager>();
        
        RefreshUI();    
        Add(newAbility);
        Remove(abilityToDiscard);
    }

    private void Update() {
        // One less than hotbar slots length bc dash ability takes up a slot
        NUMBER_OF_ABILITIES = hotbarSlots.Length - 1;
        abilityCursor.SetActive(isMovingItem);
        abilityCursor.transform.position = Input.mousePosition; 
        if (isMovingItem && managerActive) {
            abilityCursor.GetComponent<Image>().sprite = movingSlot.GetAbility().getActiveAbility().aSprite;
        } else if (isMovingItem && !managerActive) {
            EndItemMove();
        }
        if (Input.GetMouseButtonDown(0) && managerActive) 
        {
            if (isMovingItem) {
                EndItemMove();
            } else {
                BeginItemMove();
            }
        }
    }

    #region Active / Passive Utils

    public void RefreshEnabledAugments() {

        augmentManager.clearAugments();
        for (int i = 0; i < inventorySlots.Length - hotbarSlots.Length + 1; i++) {
            AbilityWrapper slot = inventorySlots.Items[i];
            if (slot != null) {
                if (DEBUG) Debug.Log("Found Ability <" + slot + "> in abilities list");
                if (slot.getPassiveAbility() == null) {
                    if (DEBUG) Debug.Log("[AbilityInventoryManager] Ability " + slot + " has no associated passive. This shouldn't happen");
                    continue;
                }
                augmentManager.addAugment(slot.getPassiveAbility());
            };
        }
        // Realistically we want this to be only applied to the AugmentManager attached to the player, so this is safe
        

    }

    #endregion

    #region Inventory Utils

    public void RefreshUI()
    {
        // no longer needed!!!!!!!! WAHOOO
        // for (int i = 0; i < slots.Length; i++) {
        //     try {
        //         //slots[i].transform.GetChild(0).GetComponent<Image>().sprite = abilities[i].GetAbility().aSprite;
        //         slots[i].transform.GetChild(0).GetComponent<Image>().sprite = abilities[i].GetAbility().getActiveAbility().aSprite;
        //         slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
        //     } catch {
        //         slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
        //         slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
        //     }
        // }

        // who needs debugging???
        // bool allClear = true;
        // foreach (SlotClass slot in abilities) {
        //     if (slot.isClear() == false) {
        //         if (DEBUG) Debug.Log("[AbilityInventoryManager/RefreshUI] Found ability <" + slot + "> in passive List");
        //         allClear = false;
        //     }
        // }
        // if (allClear) if (DEBUG) Debug.Log("[AbilityInventoryManager/RefreshUI] All slots cleared");
        RefreshEnabledAugments();
        RefreshHotBar();
    }

    public void RefreshHotBar() 
    {
        // Refresh the dash ability on the hotbar
        hotbarAbilities[0].GetAbility().getActiveAbility().Init();
        hotbarAbilities[0].GetAbility().getActiveAbility().SetState(AbilityState.ready);
        hotbarSlots[0].transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
        hotbarSlots[0].GetComponent<TooltipFormatter>().Ability = dashAbility;


        // Start at 1 to account for the dash ability taking up a slot
        for (int i = 1; i < hotbarSlots.Length; i++) {
            try {
                //slots[i].transform.GetChild(0).GetComponent<Image>().sprite = abilities[i].GetAbility().aSprite;
                hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventorySlots.Items[(i - 1) + NUMBER_OF_ABILITIES * 2].getActiveAbility().aSprite;
                hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;

                // CHANGE THIS IMMEDIATELY
                hotbarAbilities[i] = new SlotClass(inventorySlots.Items[(i - 1) + NUMBER_OF_ABILITIES * 2]);
                hotbarSlots[i].GetComponent<TooltipFormatter>().Ability = hotbarAbilities[i].GetAbility();

                if (hotbarAbilities[i].GetAbility() != null) {
                    hotbarAbilities[i].GetAbility().getActiveAbility().Init();
                    hotbarAbilities[i].GetAbility().getActiveAbility().SetState(AbilityState.ready);
                    hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
                    hotbarSlots[i].GetComponent<TooltipFormatter>().Ability = hotbarAbilities[i].GetAbility();
                }

            } catch {
                hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                hotbarSlots[i].GetComponent<TooltipFormatter>().Ability = null;
            }
        }        
    }

    // Update is called once per frame
    public bool Add(AbilityWrapper ability)
    {
        //abilities.Add(ability);
        SlotClass slot = Contains(ability);
        if (slot != null) {

        } else {
            for (int i = 0; i < inventorySlots.Length; i++) {
                if (inventorySlots[i] == null) {
                    inventorySlots.Items[i] = ability;
                    break;
                }
            }    
        }
        
        RefreshUI();
        return true;
    }

    public bool Remove(AbilityWrapper ability)
    {
        //abilities.Remove(ability); 
        SlotClass temp = Contains(ability);
        if (temp != null) {
            int slotToRemoveIndex = 0;

            for (int i = 0; i < inventorySlots.Length; i++) {
                if (inventorySlots.Items[i] == ability) {
                    slotToRemoveIndex = i;
                    break;
                }
            }
            inventorySlots[slotToRemoveIndex].Item = null;
        } else {
            return false;
        }

        RefreshUI();
        return true;

    }

    // TODO: Change this to remove SlotClass ref
    public SlotClass Contains(AbilityWrapper ability) 
    {
        for (int i = 0; i < inventorySlots.Length; i++) {
            if (inventorySlots.Items[i] == ability) {
                return new SlotClass(inventorySlots.Items[i]);
            }
        }

        return null;
    }

    #endregion Inventory Utils

    #region Drag And Drop
    // TODO: Remove SlotClass Reference
    private Slot<AbilityWrapper> GetClosestSlot() {
        for (int i = 0; i < inventorySlots.Length; i++) {
            if (Vector2.Distance(inventorySlots[i].gameObject.transform.position, Input.mousePosition) <= 32) {
                return inventorySlots[i];
            }
        }
        return null;
    }

    private bool BeginItemMove() {
        originalSlot = GetClosestSlot();
        if (originalSlot == null || originalSlot.Item == null) {
            return false;
        } 
        movingSlot = new SlotClass(originalSlot.Item); 
        originalSlot.Item = null;
        isMovingItem = true;
        RefreshUI();
        return true;
    }

    private bool EndItemMove() {
        originalSlot = GetClosestSlot();
        if (originalSlot == null) {
            Add(movingSlot.GetAbility());
            movingSlot.Clear();
        } else {
            if (originalSlot.Item != null) {
                if (originalSlot.Item == movingSlot.GetAbility()) {

                } else {
                    tempSlot = new SlotClass(originalSlot.Item);
                    originalSlot.Item = movingSlot.GetAbility();
                    movingSlot.AddAbility(tempSlot.GetAbility());
                    RefreshUI();
                    return true; 
                }
            } else {
                originalSlot.Item = movingSlot.GetAbility();
                movingSlot.Clear();
            }
        }


        isMovingItem = false;
        RefreshUI();
        return true;
    }
    #endregion Drag And Drop

    public SlotClass[] GetHotbarAbilities() {
        return hotbarAbilities;
    }

    public GameObject[] GetHotbarSlots() {
        return hotbarSlots;
    }

    public void SetManagerActive(bool active) {
        managerActive = active;
    }
}
