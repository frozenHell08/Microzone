using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    public static bool isReturning;
    [SerializeField] GameObject welcomeScreen;
    [SerializeField] GameObject loadedScreen;

    void Awake() {
        if (isReturning) {
            welcomeScreen.SetActive(false);
            loadedScreen.SetActive(true);

        } else {}
    }
}
