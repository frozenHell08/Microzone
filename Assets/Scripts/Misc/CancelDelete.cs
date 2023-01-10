using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelDelete : MonoBehaviour
{
    [Header("Details")]
    [SerializeField] private GameObject slot1;
    [SerializeField] private GameObject slot2;
    [SerializeField] private GameObject slot3;
    [SerializeField] private GameObject slot4;
    [SerializeField] private GameObject confirmation;
    [SerializeField] private GameObject refresh;

    public void Cancel() {
        refresh.SetActive(false);
        refresh.SetActive(true);
        confirmation.SetActive(false);
    }
}
