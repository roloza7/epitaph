using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    [SerializeField] private float chainAttackTime = 0.1f;
    [SerializeField] private List<GameObject> meleeHitboxes;
    [SerializeField] private float attackDelay; // how long attacks actually last
    [SerializeField] private float attackShiftMultiplier;
    private PlayerInput playerInput;
    private Camera mainCam;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private int currentAttack;
    private bool hasBufferAttack;
    private bool canChainAttack;
    private bool isAttacking;
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
        isAttacking = false;
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
        if(!canMove) {
            rb.velocity = Vector3.zero;
            return;
        }

        if (isAttacking) {
            return;
        }

        if (canChangeDirection) {
            rb.velocity = movementInput * stats.GetStatValue(StatEnum.WALKSPEED);
        } else {
            rb.velocity = lastMovementInput * stats.GetStatValue(StatEnum.WALKSPEED);
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
            if (!isAttacking) {
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
        isAttacking = true;
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector2 mousePosNorm = (new Vector2(mousePos.x, mousePos.y)).normalized;
        rb.velocity = mousePosNorm * attackShiftMultiplier;
        MeleeAttack meleeAttack = meleeHitboxes[currentAttack].GetComponent<MeleeAttack>();
        meleeAttack.SetActive();
        meleeAttack.Attack(mousePos);
        yield return new WaitForSeconds(attackDelay);
        meleeAttack.SetInactive();
        rb.velocity = Vector2.zero;
        isAttacking = false;
        StartCoroutine(AttackChainTimeWindow());
    }
    IEnumerator BufferAttack() {
        hasBufferAttack = true;
        yield return new WaitUntil(() => isAttacking == false);
        isAttacking = true;
        StartCoroutine(AttackDelay());
        hasBufferAttack = false;
    }
    IEnumerator AttackChainTimeWindow() {
        canChainAttack = true;
        yield return new WaitForSeconds(chainAttackTime);
        canChainAttack = false;
    }
}