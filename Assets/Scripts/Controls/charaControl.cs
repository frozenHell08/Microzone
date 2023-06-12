using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charaControl : MonoBehaviour {

    [Header("Controls")]
    [SerializeField] private float speedLimiter;

    [Header("Character")]
    [SerializeField] private Movement movementAnimation;

    private Rigidbody rb;
    private Animator animator;
    private LevelLogic _levelLogic;

    float inputHorizontal;
    float inputVertical;
    string currentState;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        _levelLogic = GameObject.Find("GameController").GetComponent<LevelLogic>();
    }

    void Update() {
        if (_levelLogic.GetStageStatus()) return;

        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() {
        Vector3 movementDirection = new Vector3(inputHorizontal, 0, inputVertical).normalized;
        Vector3 movementVelocity = movementDirection * speedLimiter;

        rb.velocity = movementVelocity;

        if (movementDirection != Vector3.zero) {
            if (movementDirection.x > 0) {
                ChangeAnimationState(movementAnimation.right);
            } else if (movementDirection.x < 0) {
                ChangeAnimationState(movementAnimation.left);
            } else if (movementDirection.z > 0) {
                ChangeAnimationState(movementAnimation.up);
            } else if (movementDirection.z < 0) {
                ChangeAnimationState(movementAnimation.down);
            }
        } else {
            ChangeAnimationState(movementAnimation.idle);
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}