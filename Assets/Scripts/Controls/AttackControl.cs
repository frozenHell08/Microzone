using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControl : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private LevelLogic _levelLogic;

    float speed = 0f;
    float attackTime = 1;
    float attackCounter = 1f;
    bool isAttacking;

    void Start() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        _levelLogic = GameObject.Find("GameController").GetComponent<LevelLogic>(); 
    }

    void Update() {
        rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed * Time.deltaTime;
        animator.SetFloat("moveX", rb.velocity.x);
        animator.SetFloat("moveY", rb.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) {
            animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }

        if (isAttacking) {
            rb.velocity = Vector3.zero;
            attackCounter -= Time.deltaTime;

            if (attackCounter <= 0) {
                animator.SetBool("IsAttacking", false);
                isAttacking = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (_levelLogic.GetStageStatus()) return;
            
            attackCounter = attackTime;
            animator.SetBool("IsAttacking", true);
            isAttacking = true;
        }
    }
}
