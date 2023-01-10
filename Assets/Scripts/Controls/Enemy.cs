using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void OnCollisionEnter(Collision col) {
        
        if (col.gameObject.name == "Sphere") {
            Debug.Log("BOOP !");
            Destroy(col.gameObject);
        }
    }
}
