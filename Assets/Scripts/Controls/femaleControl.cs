using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class femaleControl : MonoBehaviour
{
    Rigidbody rb;

    // Player
    [SerializeField] private int speedLimiter;
    float inputHorizontal;
    float inputVertical;

    Animator animator;
    string currentState;
    const string Female_Idle = "Female_idle";
    const string Female_Walk_left = "Female_Walk_left";
    const string Female_walk_right = "Female_walk_right";
    const string Female_walk_up = "Female_walk_up";
    const string Female_walk_down = "Female_walk_down";

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButton(0)) {
            Ray ray = 
        }
    }
    void FixedUpdate()
    {
        if (inputHorizontal != 0 || inputVertical != 0)
        {
            if (inputHorizontal != 0 || inputVertical != 0)
            {
                inputHorizontal *= speedLimiter;
                inputVertical *= speedLimiter;
            }
            rb.velocity = new Vector3(inputHorizontal, 0, inputVertical);

            if (inputHorizontal > 0)
            {
                ChangeAnimationState("Female_walk_right");
            }
            else if (inputHorizontal < 0)
            {
                ChangeAnimationState("Female_Walk_left");
            }
            else if (inputVertical > 0)
            {
                ChangeAnimationState("Female_walk_up");
            }
            else if (inputVertical < 0)
            {
                ChangeAnimationState("Female_walk_down");
            }
        }
        else
        {
            rb.velocity = new Vector3(0f, 0f);
            ChangeAnimationState("Female_Idle");
        }

    }
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
