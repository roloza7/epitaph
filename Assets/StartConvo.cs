using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartConvo : MonoBehaviour
{
    public float distanceAway = 3f;

    private GameObject target;

    public TextMeshProUGUI textComponent;

    public GameObject textbox;

    public string[] convoLines;

    DialogLogic DialogLogicScript;
    private bool isKeyEnabled = true;

    private string key = "e";

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        rb = target.GetComponent<Rigidbody>();
        textComponent.text = string.Empty;
        DialogBox = GameObject.FindGameObjectWithTag("DialogBox");
        DialogLogicScript = GameObject.FindGameObjectWithTag("DialogBox").GetComponent<DialogLogic>();
    }

    // Update is called once per frame
    void Update()
    {

        
        float dist = Vector3.Distance(transform.position, target.transform.position);
        while (DialogBox.activeInHierarchy == False){
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        if (dist < distanceAway)
        {
            textComponent.text ="Press E to Interact";
            if (isKeyEnabled)
                {
                    if (Input.GetKeyDown("e"))
                    {
                        DisableKey();
                        Debug.Log("started convo");
                        textbox.SetActive(true);

                        //change the list dialog within the DialogLogic Script to match this dialog stated in this script
                        DialogLogicScript.lines = convoLines;
                        DialogLogicScript.StartDialog();
                    }
                }
        }
        else{
            textComponent.text = string.Empty;
        }
    }

    public void DisableKey()
    {
        isKeyEnabled = false;
    }

    // Call this method to enable the key
    public void EnableKey()
    {
        isKeyEnabled = true;
    }
    
}