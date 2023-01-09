using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClearFields : MonoBehaviour
{
    [SerializeField] private List<TMP_InputField> fields;

    void OnEnable() {
        foreach (TMP_InputField _field in fields) {
            _field.text = "";
        }
    }
}
