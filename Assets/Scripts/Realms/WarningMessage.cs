using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProfileCreation;

public class WarningMessage : MonoBehaviour
{
    [SerializeField] private RealmController rController;
    [SerializeField] private TMP_Text warning;

    [Header("Save Slots")]
    [SerializeField] private GameObject saveSlot1;
    [SerializeField] private GameObject saveSlot2;
    [SerializeField] private GameObject saveSlot3;
    [SerializeField] private GameObject saveSlot4;

    [Header("Child")] [Range(0, 3)]
    [SerializeField] private int childIndex;

    private List<ProfileModel> loaded_profiles;
    private ProfileModel profile;

    void OnEnable() {
        int deleteIndex = LoadGameFiles.trigerredGarbage;
        loaded_profiles = rController.Saves();
        profile = loaded_profiles[deleteIndex];

        string msg = string.Format("Attempting to delete character [{0}].\n\nProceed?", profile.Username);
        warning.text = msg;

        OutofFocus(saveSlot1, childIndex);
        OutofFocus(saveSlot2, childIndex);
        OutofFocus(saveSlot3, childIndex);
        OutofFocus(saveSlot4, childIndex);
    }

    private void OutofFocus(GameObject slot, int _childIndex) {
        Button btn1 = slot.GetComponent<Button>();
        Button child = slot.transform.GetChild(_childIndex).GetComponent<Button>();

        btn1.interactable = false;
        child.interactable = false;
    }
}
