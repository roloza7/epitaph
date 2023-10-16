using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class AbilityHolder : MonoBehaviour
{

    [Header("Dash Ability")]
    private Slot<AbilityWrapper> dashAbility;

    [Header("Mutable Abilities")]
    private Slot<AbilityWrapper>[]  mutableAbilities = new Slot<AbilityWrapper>[3];
    // [Header("Ability 1")]
    // private Ability ability1;
    // private Image ability1Img;
    
    // [Header("Ability 2")]
    // private Ability ability2;
    // private Image ability2Img;

    // [Header("Ability 3")]
    // private Ability ability3;
    // private Image ability3Img;

    // [Header("Dash Ability")]
    // private Ability dashAbility;
    // private Image dashAbilityImage;

    GameObject parent;

    [SerializeField] private GameObject abilitySelection;
    [SerializeField] private AbilityInventoryManager abilityManager;
    public bool abilityInventoryActive {get; private set;}

    // Start is called before the first frame update
    void Awake() {
        abilityInventoryActive = false;
    }
    void Start()
    {
        // if (abilityManager.GetHotbarAbilities()[0].GetAbility() != null) {
        //     ability1 = abilityManager.GetHotbarAbilities()[0].GetAbility();
        //     ability1Img = abilityManager.GetHotbarSlots()[0].GetComponent<Image>();
        // }

        // if (abilityManager.GetHotbarAbilities()[1].GetAbility() != null) {
        //     ability2 = abilityManager.GetHotbarAbilities()[1].GetAbility();
        //     ability2Img = abilityManager.GetHotbarSlots()[1].GetComponent<Image>();
        // }

        // if (abilityManager.GetHotbarAbilities()[2].GetAbility() != null) {
        //     ability3 = abilityManager.GetHotbarAbilities()[2].GetAbility();
        //     ability3Img = abilityManager.GetHotbarSlots()[2].GetComponent<Image>();
        // }




        // ability1Img.sprite = ability1.aSprite;
        // ability2Img.sprite = ability2.aSprite;
        // ability3Img.sprite = ability3.aSprite;


        // ability1Img.fillAmount = 0;
        // ability2Img.fillAmount = 0;
        // ability3Img.fillAmount = 0;
        
        // ability1.SetState(AbilityState.ready);
        // ability2.SetState(AbilityState.ready);
        // ability3.SetState(AbilityState.ready);
        parent = this.gameObject;

//Gotta handle making sure this happens first time ability is assignec
        // ability1.Init();
        // ability2.Init();
        // ability3.Init();
    }

    // Update is called once per frame
    private void Render() {
        if (dashAbility == null) {
            dashAbility = abilityManager.hotbar.GetDashAbilitySlot();
            dashAbility.SetFillAmount(1);
        }
        for (int i = 0; i < mutableAbilities.Length; i++) {
            if (mutableAbilities[i] == null)
                mutableAbilities[i] = abilityManager.hotbar.GetMutableAbilitySlot(i);

            Slot<AbilityWrapper> slot = mutableAbilities[i];
            if (slot.IsClear() == false) {
                slot.SetFillAmount(1);
            }

        }
    }
    void Update()
    {  
        Render();

        if (dashAbility.IsClear() == false) {
            dashAbility.Item.ActiveAbility.AbilityCooldownHandler(parent);
            dashAbility.Item.ActiveAbility.AbilityBehavior(parent);
            dashAbility.SetFillAmount(dashAbility.Item.ActiveAbility.fillAmount);
        }

        foreach (Slot<AbilityWrapper> slot in mutableAbilities) {
            if (slot.IsClear() == false) {
                slot.Item.ActiveAbility.AbilityCooldownHandler(parent);
                slot.Item.ActiveAbility.AbilityBehavior(parent);
                slot.SetFillAmount(slot.Item.ActiveAbility.fillAmount);
            }
        }

    }

    private void OnAbility(Slot<AbilityWrapper> slot, InputAction.CallbackContext context) {

        if (slot.IsClear() == true)
            return;

        Ability ability = slot.Item.ActiveAbility;


        if (context.started)
            ability.SetAbilityPressed(true);
        else if (context.canceled)
            ability.SetAbilityPressed(false);


    }
    public void OnAbility1(InputAction.CallbackContext context) 
    {
        OnAbility(mutableAbilities[0], context);
    }

    public void OnAbility2(InputAction.CallbackContext context) 
    {
        OnAbility(mutableAbilities[1], context);
    }

    public void OnAbility3(InputAction.CallbackContext context) 
    {
        OnAbility(mutableAbilities[2], context);
    }

    public void OnDashAbility(InputAction.CallbackContext context) {
        OnAbility(dashAbility, context);
    }

    public void OnAbilityInventory(InputAction.CallbackContext context) {
        abilityInventoryActive = !abilityInventoryActive;
        EnableInventory(abilityInventoryActive);
       // abilitySelection.SetActive(abilityInventoryActive);
    }

    // Programmaticaly triggered inventory - for scene transitions
    public void OnAbilityInventory() {
        abilityInventoryActive = !abilityInventoryActive;
        EnableInventory(abilityInventoryActive);
    }

    public void EnableInventory(bool active) {
        abilitySelection.SetActive(active);
        abilityManager.SetManagerActive(active);
    }

}   

// if (dashAbility == null) {
        //     dashAbility = abilityManager.GetHotbarAbilities()[0].GetAbility().getActiveAbility();
        //     dashAbilityImage = abilityManager.GetHotbarSlots()[0].transform.GetChild(0).GetComponent<Image>();
        //     dashAbility.fillAmount = 1;
        // }

        // if (abilityManager.GetHotbarAbilities()[1].GetAbility() != null) {
        //     ability1 = abilityManager.GetHotbarAbilities()[1].GetAbility().getActiveAbility();
        //     ability1Img = abilityManager.GetHotbarSlots()[1].transform.GetChild(0).GetComponent<Image>();
        //     ability1Img.fillAmount = 1;
        // } else {
        //     ability1 = null;
        // }

        // if (abilityManager.GetHotbarAbilities()[2].GetAbility() != null) {
        //     ability2 = abilityManager.GetHotbarAbilities()[2].GetAbility().getActiveAbility();
        //     ability2Img = abilityManager.GetHotbarSlots()[2].transform.GetChild(0).GetComponent<Image>();
        //     ability2Img.fillAmount = 1;
        // } else {
        //     ability2 = null;
        // }

        // if (abilityManager.GetHotbarAbilities()[3].GetAbility() != null) {
        //     ability3 = abilityManager.GetHotbarAbilities()[3].GetAbility().getActiveAbility();
        //     ability3Img = abilityManager.GetHotbarSlots()[3].transform.GetChild(0).GetComponent<Image>();
        //     ability3Img.fillAmount = 1;
        // } else {
        //     ability3 = null;
        // }