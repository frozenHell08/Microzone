using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controls : MonoBehaviour
{
    // Components
    Rigidbody rb;

    // Player
    [SerializeField] private int speedLimiter;
    float inputHorizontal;
    float inputVertical;

    // Animations and states
    Animator animator;
    string currentState;
    const string PLAYER_IDLE = "player_idle";
    const string PLAYERWALK_LEFT = "playerwalk_left";
    const string PLAYERWALK_RIGHT = "playerwalk_right";
    const string PLAYERWALK_FRONT = "playerwalk_front";
    const string PLAYERWALK_BACK = "playerwalk_back";

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

    }

    void FixedUpdate()
    {
        if (inputHorizontal != 0 || inputVertical !=0)
        {
            if(inputHorizontal != 0 || inputVertical !=0)
            {
                inputHorizontal *= speedLimiter;
                inputVertical *= speedLimiter;
            }
            rb.velocity = new Vector3(inputHorizontal, 0, inputVertical);

            if (inputHorizontal > 0)
            {
                ChangeAnimationState(PLAYERWALK_RIGHT);
            }

            else if (inputHorizontal < 0)
            {
                ChangeAnimationState(PLAYERWALK_LEFT);
            }

            else if (inputVertical > 0)
            {
                ChangeAnimationState(PLAYERWALK_BACK);
            }

            else if (inputVertical < 0)
            {
                ChangeAnimationState(PLAYERWALK_FRONT);
            }
        }
        else
        {
            rb.velocity = new Vector3(0f, 0f);
            ChangeAnimationState(PLAYER_IDLE);
        }
    }

    // Animation state changer
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
