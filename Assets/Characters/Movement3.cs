using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3 : MonoBehaviour
{
    Rigidbody rb;

    // Player
    float speedLimiter = 50f;
    float inputHorizontal;
    float inputVertical;

    // Animations and states
    Animator animator;
    string currentState;
    const string idle = "idle";
    const string Mwalk_left = "Mwalk_left";
    const string Mwalk_right = "Mwalk_right";
    const string Mwalk_down = "Mwalk_down";
    const string Mwalk_up = "Mwalk_up";

    private LevelLogic _levelLogic;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        _levelLogic = GameObject.Find("GameController").GetComponent<LevelLogic>();
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
            if (inputHorizontal != 0 && inputVertical != 0)
            {
                inputHorizontal *= speedLimiter;
                inputVertical *= speedLimiter;
            }
            rb.velocity = new Vector3(inputHorizontal, 0, inputVertical);

            if (inputHorizontal > 0)
            {
                ChangeAnimationState(Mwalk_right);
            }

            else if (inputHorizontal < 0)
            {
                ChangeAnimationState(Mwalk_left);
            }

            else if (inputVertical > 0)
            {
                ChangeAnimationState(Mwalk_up);
            }

            else if (inputVertical < 0)
            {
                ChangeAnimationState(Mwalk_down);
            }
        }
        else
        {
            rb.velocity = new Vector3(0f, 0f);
            ChangeAnimationState(idle);
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

    void OnCollisionEnter (Collision col) {
        if (col.gameObject.tag == "Enemy") {
            Destroy(col.gameObject);
            Debug.Log("boop");
            _levelLogic.AddKill();
        } else if (col.gameObject.tag == "Player") {
            Debug.Log("MINUS");
        }
    }
}
