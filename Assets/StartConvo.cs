using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartConvo : MonoBehaviour
{
    public float distanceAway = 3f;

    private GameObject target;

    public TextMeshProUGUI textComponent;

    private GameObject textbox;

    public string[] convoLines;
    public string[] convoLinesNames;

    public List<Sprite> allSprites;

    DialogLogic DialogLogicScript;
    private bool isKeyEnabled = true;

    public bool useHitboxToInteract = true;

    private GameObject DialogBox;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        textbox = GameObject.FindGameObjectWithTag("DialogBox");
        //textComponent = this.GetComponent<TextMeshProUGUI>();
        target = GameObject.FindWithTag("Player");
        rb = target.GetComponent<Rigidbody2D>();
        textComponent.text = string.Empty;
        DialogBox = GameObject.FindGameObjectWithTag("DialogBox");
        DialogLogicScript = GameObject.FindGameObjectWithTag("DialogBox").GetComponent<DialogLogic>();
    }

    //Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (useHitboxToInteract != true){
            if (dist < distanceAway)
            {
                textComponent.text ="Press Space to Interact";
                if (isKeyEnabled)
                    {
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            Debug.Log("pressed Space in range");
                            DisableKey();
                            textbox.SetActive(true);

                            //change the list dialog within the DialogLogic Script to match this dialog stated in this script
                            DialogLogicScript.lines = convoLines;
                            DialogLogicScript.namesPerLine = convoLinesNames;
                            DialogLogicScript.Sprites = allSprites;
                            DialogLogicScript.StartDialog();
                        }
                    }
            }
            else{
                textComponent.text = string.Empty;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (useHitboxToInteract){
            Debug.Log("triggered NPC");
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("started convo");
                textbox.SetActive(true);

                //change the list dialog within the DialogLogic Script to match this dialog stated in this script
                DialogLogicScript.lines = convoLines;
                DialogLogicScript.namesPerLine = convoLinesNames;
                DialogLogicScript.Sprites = allSprites;
                DialogLogicScript.StartDialog();
            }
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