using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfirmExitStage : MonoBehaviour
{
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private Warning warning;

    private bool isPaused = false;

    void OnEnable() {
        TMP_Text message = messagePanel.GetComponentsInChildren<TMP_Text>(true).FirstOrDefault(txt => txt.name.Equals("confirmMessage"));
        message.text = warning.message;
        Time.timeScale = 1f;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                CancelExit();
                Resume();
            } else {
                Pause();
                AttemptExit();
            }
        }
    }

    public void AttemptExit() {
        messagePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CancelExit() {
        messagePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Pause() {
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void Resume() {
        Time.timeScale = 1f;
        isPaused = false;
    }
}
