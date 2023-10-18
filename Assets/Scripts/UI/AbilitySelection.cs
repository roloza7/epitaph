using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AbilitySelection : MonoBehaviour
{

    [SerializeField] private GameObject root;

    private SlotHolder<AbilityWrapper> choiceSlots;
    public SlotHolder<AbilityWrapper> Slots { get { return choiceSlots; } }

    [SerializeField] private List<AbilityWrapper> abilityChoices;
    private List<AbilityWrapper> options = new List<AbilityWrapper>();
    private bool choiceMade;
    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ShowAbilityChoice();
        //Debug.Log("OnSceneLoaded: " + scene.name);
        choiceMade = false;
        //Debug.Log("count" + abilityChoices.Count);

        for (int i = 0; i < 3; i++) {
            if (abilityChoices.Count > 0) {
                int randomIndex = Random.Range(0, abilityChoices.Count-1);
                choiceSlots[i].Item = abilityChoices[randomIndex];
                choiceSlots[i].formatter.Ability = abilityChoices[randomIndex];
                abilityChoices.RemoveAt(randomIndex);
            }
        }

        
    }
    void Awake()
    {
        choiceSlots = new SlotHolder<AbilityWrapper>(root);
    }

    public void HideAbilityChoice() {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ShowAbilityChoice() {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void setAbilityChoices(List<AbilityWrapper> abilityChoices) {
        this.abilityChoices = abilityChoices;
    }

}
