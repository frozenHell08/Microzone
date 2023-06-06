using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfirmExitStage : MonoBehaviour
{
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private Warning warning;

    void OnEnable() {
        TMP_Text message = messagePanel.GetComponentsInChildren<TMP_Text>(true).FirstOrDefault(txt => txt.name.Equals("confirmMessage"));
        message.text = warning.message;
    }

    public void AttemptExit() {
        messagePanel.SetActive(true);
        // pause game also triggers on esc button
    }

    public void CancelExit() {
        messagePanel.SetActive(false);
        //resume game
    }
}
