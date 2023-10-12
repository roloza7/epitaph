using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float chainAttackTime = 0.1f;
    [SerializeField] private List<GameObject> meleeHitboxes;
    private PlayerInput playerInput;
    private Camera mainCam;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private int currentAttack;
    private bool hasBufferAttack;
    private bool canChainAttack;
    private Vector2 lastMovementInput;

    public Vector2 LastMovementInput {
        get {
            return lastMovementInput;
        }
    }
    void Start()
    {
        canMove = true;
        canChangeDirection = true;
        playerInput = GetComponent<PlayerInput>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        movementInput = Vector2.zero;
        currentAttack = 0;
        hasBufferAttack = false;
        canChainAttack = false;
        entity = GetComponent<Player>();
        stats = entity.EntityStats;
    }

    public void resetPos()
    {
        this.transform.position = new Vector3(0, 0, 0);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(canMove == true) {
            if (canChangeDirection) {
                rb.velocity = movementInput * stats.GetStatValue(StatEnum.WALKSPEED);
            } else {
                rb.velocity = lastMovementInput * stats.GetStatValue(StatEnum.WALKSPEED);
            }
        } else {
            rb.velocity = Vector3.zero;
        }
    }

    public void OnMove(InputAction.CallbackContext ctx) {
        movementInput = ctx.ReadValue<Vector2>();
        if (canChangeDirection){
            lastMovementInput = movementInput;
        }
    }

    public void OnMelee(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            if (canMove == true) {
                canMove = false;
                StartCoroutine(AttackDelay());
            } else if (!hasBufferAttack) {
                StartCoroutine(BufferAttack());
            }
        }
    }
    IEnumerator AttackDelay() {
        if (canChainAttack) {
            if (currentAttack >= meleeHitboxes.Count - 1) {
            currentAttack = 0;
            } else {
                currentAttack++;
            }
        } else {
            currentAttack = 0;
        }
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        MeleeAttack meleeAttack = meleeHitboxes[currentAttack].GetComponent<MeleeAttack>();
        meleeAttack.SetActive();
        meleeAttack.Attack(mousePos);
        yield return new WaitForSeconds(0.3f);
        meleeAttack.SetInactive();
        canMove = true;
        StartCoroutine(AttackChainTimeWindow());
    }
    IEnumerator BufferAttack() {
        hasBufferAttack = true;
        yield return new WaitUntil(() => canMove == true);
        canMove = false;
        StartCoroutine(AttackDelay());
        hasBufferAttack = false;
    }
    IEnumerator AttackChainTimeWindow() {
        canChainAttack = true;
        yield return new WaitForSeconds(chainAttackTime);
        canChainAttack = false;
    }
}