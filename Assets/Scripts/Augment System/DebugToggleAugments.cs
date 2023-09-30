using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DebugToggleAugments : MonoBehaviour
{
    [SerializeField]
    private AugmentManager augmentManager;
    
    private bool active = false;

    private void Awake()
    {
        this.augmentManager = gameObject.GetComponent<AugmentManager>();
    }
    /**
     * Add this to Player and use Space Bar to toggle coroutines
     * After we have a proper system for runs we can scrap this
     **/
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!active) {
                Debug.Log("Starting Run");
                augmentManager.onRunStart();
            }
            else {
                Debug.Log("Stopping Run");
                augmentManager.onRunEnd();
            } 
            active = !active;
        }
    }
}
