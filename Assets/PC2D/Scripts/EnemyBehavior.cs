using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    public Transform player;
    public float moveSpeed = 1.63f;
    public float aggroDistance = 7.75f;

    private Animator animator;
    private float distance;

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance < aggroDistance) {
            transform.Translate(new Vector3(moveSpeed * 3, 0f, 0f) * Time.deltaTime);
            animator.Play("run");
        }
        else {
            transform.Translate(new Vector3(moveSpeed, 0f, 0f) * Time.deltaTime);
            animator.Play("walk");
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Patrol") {
            moveSpeed *= -1;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
    }
}
