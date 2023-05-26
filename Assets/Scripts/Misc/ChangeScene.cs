using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    [SerializeField] private Static isReturning;

    public void GoingBack(string destination) {
        if (destination == "Menu") {
            isReturning.boolValue = true;
        }

        SceneManager.LoadScene(destination);
    }
}
