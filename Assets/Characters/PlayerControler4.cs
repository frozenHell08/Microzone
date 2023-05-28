using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler4 : MonoBehaviour
{
    public Rigidbody rb;
    private Animator myAnim;

    private float speed = 0f;

    private float attackTime = 1;
    private float attackCounter = 1f;
    private bool IsAttacking;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed * Time.deltaTime;
        myAnim.SetFloat("moveX", rb.velocity.x);
        myAnim.SetFloat("moveY", rb.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }

        if (IsAttacking)
        {
            rb.velocity = Vector3.zero;
            attackCounter -= Time.deltaTime;
            if(attackCounter <= 0)
            {
                myAnim.SetBool("IsAttacking", false);
                IsAttacking = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            attackCounter = attackTime;
            myAnim.SetBool("IsAttacking", true);
            IsAttacking = true;
        }
    }
}
