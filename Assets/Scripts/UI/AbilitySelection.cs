using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AbilitySelection : MonoBehaviour
{
    [SerializeField] private List<AbilityWrapper> abilityChoices;
    [SerializeField] private AbilityInventoryManager abilityManager;

    [SerializeField] private Button choice1;
    [SerializeField] private Button choice2;
    [SerializeField] private Button choice3;
    private List<AbilityWrapper> options = new List<AbilityWrapper>();
    private bool choiceMade;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        choiceMade = false;

        for (int i = 0; i < 3; i++) {
            if (abilityChoices.Count > 0) {
                int randomIndex = Random.Range(0, abilityChoices.Count-1);
                options.Add(abilityChoices[randomIndex]);
                abilityChoices.RemoveAt(randomIndex);
            }
        }

        choice1.image.sprite = options[0].getActiveAbility().aSprite;
        choice2.image.sprite = options[1].getActiveAbility().aSprite;
        choice3.image.sprite = options[2].getActiveAbility().aSprite;
    }
    void Awake()
    {

    
        choice1 = (Button) GameObject.Find ("Choice1").GetComponent<Button>();
        choice2 = (Button) GameObject.Find ("Choice2").GetComponent<Button>();
        choice3 = (Button) GameObject.Find ("Choice3").GetComponent<Button>();

    }

    public void Choice1() {
        if (!choiceMade) {
            abilityManager.Add(options[0]);
            options.RemoveAt(0);
            choiceMade = true;
        }
    }

    public void Choice2() {
        if (!choiceMade) {
            abilityManager.Add(options[1]);
            options.RemoveAt(1);
            choiceMade = true;

        }    
    }

    public void Choice3() {
        if (!choiceMade) {
            abilityManager.Add(options[2]);
            options.RemoveAt(2);
            choiceMade = true;
        }    
    }

    public void setAbilityChoices(List<AbilityWrapper> abilityChoices) {
        abilityChoices = abilityChoices;
    }

}
