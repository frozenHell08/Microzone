using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Male Movement */
public class Movement3 : MonoBehaviour
{
    Rigidbody rb;

    // Player
    [SerializeField] private float speedLimiter;
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
        if (_levelLogic.GetStageStatus()) return;
        
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
                ChangeAnimationState(Mwalk_right);
            } else if (movementDirection.x < 0) {
                ChangeAnimationState(Mwalk_left);
            } else if (movementDirection.z > 0) {
                ChangeAnimationState(Mwalk_up);
            } else if (movementDirection.z < 0) {
                ChangeAnimationState(Mwalk_down);
            }
        } else {
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
}
