using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void OnCollisionEnter(Collision col) {
        
        if (col.gameObject.name == "Sphere" || col.gameObject.name == "s__aureus") {
            Debug.Log("BOOP !");
            Destroy(col.gameObject);
        }
    }
}
