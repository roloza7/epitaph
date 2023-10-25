using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Controller
{
    protected bool isColliding;
    protected Enemy enemy;
    
    public bool IsColliding {
        get {return isColliding;}
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isColliding = false;
        enemy = GetComponent<Enemy>();
        canMove = true;
        stats = enemy.EntityStats;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (CanMove) {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            isColliding = true;
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            isColliding = false;
        }
    }
}
