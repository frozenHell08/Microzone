using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageValue : MonoBehaviour
{
    [Range(0, 3)]
    [SerializeField] private int deleteIndex;
    [SerializeField] private GameObject confirmation;

    public void AttemptToDelete() {
        LoadGameFiles.trigerredGarbage = deleteIndex;
        confirmation.SetActive(true);
    }
}
