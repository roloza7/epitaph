using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{

    public float timeToDisappear;
    private float created;
    // Start is called before the first frame update
    void Start()
    {
        created = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - created > timeToDisappear)
        {
            Destroy(gameObject);
        }
    }
}
