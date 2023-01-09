using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlscript : MonoBehaviour
{   

      // Components
    Rigidbody rb;

    // Player
    float speedLimiter = 10f;
    float inputHorizontal;
    float inputVertical;

    // Animations and states
    Animator animator;
    string currentState;
    const string Male_idle = "Male_Idle";
    const string Male_walk_left = "Male_walk_left";
    const string Male_walk_right = "Male_walk_right";
    const string Male_walk_up = "Male_walk_up";
    const string Male_walk_down = "Male_walk_down";


    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
    }

    
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }
     void FixedUpdate()
    {
        if (inputHorizontal != 0 || inputVertical != 0)
        {
            if(inputHorizontal !=0 && inputVertical != 0)
            {
                inputHorizontal *= speedLimiter;
                inputVertical *= speedLimiter;
            }
            rb.velocity = new Vector3(inputHorizontal, 0, inputVertical);

            if(inputHorizontal > 0)
            {
                ChangeAnimationState(Male_walk_right);
            }
            else if (inputHorizontal < 0)
            {
                ChangeAnimationState(Male_walk_left);
            }
            else if (inputVertical > 0 )
            {
                ChangeAnimationState(Male_walk_up);
            }
            else if (inputVertical < 0)
            {
                ChangeAnimationState(Male_walk_down);
            }
        }
        else
        {
            rb.velocity = new Vector3(0f, 0f);
            ChangeAnimationState(Male_idle);
        }
        
    } 
    void ChangeAnimationState(string newState)
    {
        // Stop animation interrupting itself
        if (currentState == newState) return;

        // Play new animation
        animator.Play(newState);

        // Update current state
        currentState = newState;
    }
}
