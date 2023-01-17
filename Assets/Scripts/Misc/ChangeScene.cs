using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void GoingBack(string destination) {
        if (destination == "Menu") {
            // ScreenController.isReturning = true;
        }

        SceneManager.LoadScene(destination);
    }
}
