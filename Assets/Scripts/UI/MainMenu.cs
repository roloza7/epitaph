using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<AbilityWrapper> abilityChoices;
    public void Play() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene(1);

    }

    public void Restart() {
        //ReinstateAbilityChoices();
        Destroy(GameObject.Find("UI"));

        SceneManager.LoadScene(1);
    }

    public void Menu() {
        //ReinstateAbilityChoices();

        Destroy(GameObject.Find("UI"));

        SceneManager.LoadScene(0);
    }

    public void Quit() {
        Debug.Log("quit");
        Application.Quit();
    }

    public void ReinstateAbilityChoices() {
        GameObject.Find("AbilitySelection").GetComponent<AbilitySelection>().setAbilityChoices(abilityChoices);
    }
}