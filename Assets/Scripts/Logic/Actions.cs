using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    void OnCollisionEnter(Collision col) {
        Debug.Log($"col gameobject : {col.gameObject}");
        Debug.Log($"col tag : {col.gameObject.tag}");
        Debug.Log($"gameobject tag : {gameObject.tag}");
        Debug.Log($"gameobkect : {gameObject}");
    }
}
