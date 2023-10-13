using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartConvo : MonoBehaviour
{
    public float distanceAway = 5f;

    private GameObject target;

    public TextMeshProUGUI textComponent;

    public GameObject textbox;

    public string[] convoLines;

    DialogLogic DialogLogicScript;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        textComponent.text = string.Empty;
        DialogLogicScript = GameObject.FindGameObjectWithTag("DialogBox").GetComponent<DialogLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist < distanceAway)
        {
            textComponent.text ="Press E to Interact";
            if (Input.GetKeyDown("e")){
                Debug.Log("started convo");
                textbox.SetActive(true);

                //change the list dialog within the DialogLogic Script to match this dialog stated in this script
                DialogLogicScript.lines = convoLines;
                DialogLogicScript.StartDialog();
            }
        }
        else{
            textComponent.text = string.Empty;
        }
    }
    
}