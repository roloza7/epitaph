using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    protected Entity entity;
    protected EntityStats stats;
    protected bool canMove;

    protected bool canChangeDirection;
    public bool CanMove {
        get { return canMove; }
        set { canMove = value; }
    }
    public bool CanChangeDirection {
        get { return canChangeDirection; }
        set { canChangeDirection = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
