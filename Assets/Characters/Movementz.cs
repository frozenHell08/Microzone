using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movementz : MonoBehaviour
{
    Rigidbody rb;

    // Player
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
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() {
        Vector3 movementDirection = new Vector3(inputHorizontal, 0, inputVertical).normalized;
        Vector3 movementVelocity = movementDirection * speedLimiter;

        rb.velocity = movementVelocity;

        //animation state
        if (movementDirection != Vector3.zero) {
            if (movementDirection.x > 0) {
                ChangeAnimationState(F_walk_right);
            } else if (movementDirection.x < 0) {
                ChangeAnimationState(F_walk_left);
            } else if (movementDirection.z > 0) {
                ChangeAnimationState(F_walk_up);
            } else if (movementDirection.z < 0) {
                ChangeAnimationState(F_walk_down);
            }
        } else {
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
}
