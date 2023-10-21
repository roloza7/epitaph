using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoButton : MonoBehaviour
{

    public void callOnRunStart()
    {
        GameObject levelController = GameObject.FindWithTag("LevelController");
        levelController.GetComponent<SceneTransitionManager>().OnRunStart();
    }
}
