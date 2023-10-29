using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPauseUI : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject pauseMenu; 

    private PlayerInput playerInput;

    void Start(){
        playerInput = GetComponent<PlayerInput>();
        
    }

    public void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exit(){
        Application.Quit();
    }

    public void ChangeControls(){
        //opens menu to change control scheme for when playing the game
        Debug.Log("change control");
    }
}
