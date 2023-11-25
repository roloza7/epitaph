using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnerController : MonoBehaviour
{
    public GameObject lich;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            StartCoroutine(playAnimationThenSpawn());
        }
    }

    IEnumerator playAnimationThenSpawn() {
        animator.SetBool("spawn", true);
        yield return new WaitForSeconds(5);
        Instantiate(lich, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
