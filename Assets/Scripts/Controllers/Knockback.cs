using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float knockbackDelay; // how much force is applied on knockback
    [SerializeField] private float knockbackForce; // how long "CC" is
    private Rigidbody2D body;
    private Controller controller;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller>();
    }
    public void KnockbackEntity(GameObject applier) {
        StopAllCoroutines();
        Vector2 direction = (transform.position - applier.transform.position).normalized;
        body.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(ResetKnockBack());
    }

    public void KnockbackEntityWithCustomForce(GameObject applier, float force) {
        StopAllCoroutines();
        Vector2 direction = (transform.position - applier.transform.position).normalized;
        body.AddForce(direction * force, ForceMode2D.Impulse);
        StartCoroutine(ResetKnockBack());
    }
    private IEnumerator ResetKnockBack() {
        controller.CanMove = false;
        yield return new WaitForSeconds(knockbackDelay);
        body.velocity = Vector3.zero;
        controller.CanMove = true;
    }
}
