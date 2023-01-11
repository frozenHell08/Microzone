using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class charaControl : MonoBehaviour {

    [Header("Controls")]
    [SerializeField] private Gender gender;
    [SerializeField] private int speedLimiter;
    [SerializeField] private CharacterAnimSO Walking;
    [SerializeField] private CharacterAnimSO Milaon;

    [Header("Character")]
    [SerializeField] private GameObject Female;
    [SerializeField] private GameObject Male;

    private LevelLogic _levelLogic;
    private Rigidbody rb;
    private Animator animator;

    float inputHorizontal;
    float inputVertical;
    private string currentState;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        _levelLogic = GameObject.Find("GameController").GetComponent<LevelLogic>();
    }

    void Update() {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() {
        if (inputHorizontal != 0 || inputVertical != 0) {
            if (inputHorizontal != 0 || inputVertical != 0) {
                inputHorizontal *= speedLimiter;
                inputVertical *= speedLimiter;
            }

            rb.velocity = new Vector3(inputHorizontal, 0, inputVertical);

            if (inputHorizontal > 0) {
                ChangeAnimationState(Walking.Right);
            } else if (inputHorizontal < 0) {
                ChangeAnimationState(Walking.Left);
            } else if (inputVertical > 0) {
                ChangeAnimationState(Walking.Up);
            } else if (inputVertical < 0) {
                ChangeAnimationState(Walking.Down);
            }
        } else {
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

    void OnCollisionEnter (Collision col) {
        if (col.gameObject.tag == "Enemy") {
            Destroy(col.gameObject);

            _levelLogic.AddKill();
        }
    }
}

public enum Gender { Male, Female }