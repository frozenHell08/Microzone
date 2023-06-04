using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class Virus : MonoBehaviour
{
    // public UnityEngine.AI.NavMeshAgent enemyNav;
    // public Transform Player;

    // void Start() {
    //     GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
    //     if (playerObject != null) {
    //         Player = playerObject.transform;
    //     }   
    // }

    // void Update() {
    //     enemyNav.SetDestination(Player.position);
    // }

// --------------------------------------------------

    public float detectionRange = 10f;

    private Transform player;
    private NavMeshAgent enemyNav;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyNav = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update() {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange) {
            enemyNav.enabled = true;
            enemyNav.SetDestination(player.position);
        } else {
            enemyNav.enabled = false;
        }
    }
}
