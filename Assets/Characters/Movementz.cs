using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Female Movement */
public class Movementz : MonoBehaviour
{
    Rigidbody rb;

    // Player
    // float speedLimiter = 50f;
    [SerializeField] private float speedLimiter;
    float inputHorizontal;
    float inputVertical;

    // Animations and states
    Animator animator;
    string currentState;
    const string F_idle = "F_idle";
    const string F_walk_left = "F_walk_left";
    const string F_walk_right = "F_walk_right";
    const string F_walk_down = "F_walk_down";
    const string F_walk_up = "F_walk_up";

    private LevelLogic _levelLogic;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        _levelLogic = GameObject.Find("GameController").GetComponent<LevelLogic>();
    }


    void Update()
    {
        if (_levelLogic.GetStageStatus()) return;

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
                ChangeAnimationState(F_walk_right);
            }

            else if (inputHorizontal < 0)
            {
                ChangeAnimationState(F_walk_left);
            }

            else if (inputVertical > 0)
            {
                ChangeAnimationState(F_walk_up);
            }

            else if (inputVertical < 0)
            {
                ChangeAnimationState(F_walk_down);
            }
        }
        else
        {
            rb.velocity = new Vector3(0f, 0f);
            ChangeAnimationState(F_idle);
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
