using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusGlobalDetect : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Transform player;

    void Start() {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) {
            player = playerObject.transform;
        }
    }

    void Update() {
        if (player != null) {
            transform.LookAt(player);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}
