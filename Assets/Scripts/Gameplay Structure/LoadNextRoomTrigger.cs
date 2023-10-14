using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextRoomTrigger : MonoBehaviour
{
    public void LoadNextRoom() {
        SceneTransitionManager _ = GameObject.FindGameObjectWithTag("LevelController").GetComponent<SceneTransitionManager>();
        _.LoadNextLevel();
    }
}
