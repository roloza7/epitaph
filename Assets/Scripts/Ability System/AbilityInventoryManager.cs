using UnityEngine;
using UnityEngine.UI;

public class AbilityInventoryManager : MonoBehaviour
{

    [SerializeField] private bool DEBUG = false;

    private AbilityCursor cursor;
    private GameObject hotbarSlotHolder;


    //public List<Ability> abilities11 = new List<Ability>();

    [SerializeField] private AbilityWrapper[] startingAbilities;
    [SerializeField] private AbilityWrapper dashAbility;
    // private SlotClass[] abilities;
    private SlotClass[] hotbarAbilities;

    // Getter so we can influence skill behavior using augments
    public SlotClass[] HotBarAbilities { get { return hotbarAbilities; }}

    // Fancy new classes for holding items
    public SlotHolder<AbilityWrapper> slots;

    // private GameObject[] slots;
    private GameObject[] hotbarSlots;
    private static int NUMBER_OF_ABILITIES;

    private AugmentManager augmentManager;


    private Slot<AbilityWrapper> originalSlot;
    bool isMovingItem;

    private bool managerActive;

    // Start is called before the first frame update
    private void Awake() {
        slots = new SlotHolder<AbilityWrapper>(GameObject.FindWithTag("Slots"));
        cursor = GameObject.FindWithTag("Cursor").GetComponent<AbilityCursor>();
        hotbarSlotHolder = GameObject.FindWithTag("HotBar");
        managerActive = false;
    }


    private void Start()
    {


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
            Debug.Log("Acessing Index " + i + " to set starting abilities, array of length " + slots.Length);
            slots.Items[i] = startingAbilities[i];
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
    }

    private void Update() {
        // One less than hotbar slots length bc dash ability takes up a slot
        NUMBER_OF_ABILITIES = hotbarSlots.Length - 1;
        // abilityCursor.SetActive(isMovingItem);
        // abilityCursor.transform.position = Input.mousePosition; 
        // if (isMovingItem && managerActive) {
        //     abilityCursor.GetComponent<Image>().sprite = movingSlot.Item.getActiveAbility().aSprite;
        // } else 
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
        }
    }

    #region Active / Passive Utils

    public void RefreshEnabledAugments() {

        augmentManager.clearAugments();
        for (int i = 0; i < slots.Length - hotbarSlots.Length + 1; i++) {
            AbilityWrapper slot = slots.Items[i];
            if (slot != null) {
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
                hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = slots.Items[(i - 1) + NUMBER_OF_ABILITIES * 2].getActiveAbility().aSprite;
                hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;

                // CHANGE THIS IMMEDIATELY
                hotbarAbilities[i] = new SlotClass(slots.Items[(i - 1) + NUMBER_OF_ABILITIES * 2]);
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
                Debug.LogWarning("[InventoryManager] Can't add ability: " + ability + " is already in the inventory");
                return false;
            }
        }

        slotToFill.Item = ability;
        return true;
    }

    #endregion Inventory Utils

    #region Drag And Drop
    private Slot<AbilityWrapper> GetClosestSlot() {
        for (int i = 0; i < slots.Length; i++) {
            if (Vector2.Distance(slots[i].gameObject.transform.position, Input.mousePosition) <= 32) {
                return slots[i];
            }
        }
        return null;
    }

    private void BeginItemMove() {
        originalSlot = GetClosestSlot();
        if (originalSlot.Item == null) {
            return;
        } 
        cursor.Ability = originalSlot.Item; 
        originalSlot.Item = null;
        isMovingItem = true;
        return;
    }

    private void EndItemMove() {
        Slot<AbilityWrapper> slotToFill = GetClosestSlot();
        if (slotToFill == null) {
            // Moving failed, revert
            originalSlot.Item = cursor.Ability;
            cursor.Ability = null;
        } else {
            // Found a candidate slot
            if (slotToFill.Item != null) {
                // Slot is filled, do some swapping
                AbilityWrapper temp = slotToFill.Item;
                slotToFill.Item = cursor.Ability;
                cursor.Ability = temp;
                // Don't null originalSlot, we might need it later
                return;
            } else {
                // Slot is not filled
                slotToFill.Item = cursor.Ability;
                cursor.Ability = null;
                // We can safely null originalSlot
                originalSlot.Item = null;
            }
        }
        isMovingItem = false;
        return;
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
