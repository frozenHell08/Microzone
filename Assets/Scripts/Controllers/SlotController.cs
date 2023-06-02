using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Realms.Exceptions;
using TMPro;
using ProfileCreation;

public class SlotController : MonoBehaviour
{
    [SerializeField] private RealmController rController;
    [Header("Sprites")]
    [SerializeField] private Sprite maleSprite;
    [SerializeField] private Sprite femaleSprite;
    [Header("Objects")]
    [SerializeField] private Warning warn;
    [SerializeField] private CurrentCharacter character;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private GameObject homeScreen;
    [SerializeField] private List<GameObject> panels;
    [SerializeField] private List<SaveSlot> slotFiles;
    [SerializeField] private List<SlotInfo> slotInformation;

    private List<ProfileModel> loaded_profiles;
    private ProfileModel profile;

    void OnEnable() {
        Refresh();
    }

    public void LoadCharacter(SaveSlot save) {
        character.characterID = save.ID;
        character.characterName = save.Name;
        character.gender = save.Gender;
        character.sprite = save.Sprite;
        character.cells = save.Cells;
        character.genes = save.Genes;
        character.level = save.Level;

        gameObject.SetActive(false);
        homeScreen.SetActive(true);
    }

    public void TriggerDelete(SaveSlot save) {
        warn.message = $"Attempting to delete character [{save.Name} ({save.Gender})]. \n\nProceed?";

        TMP_Text msg = warningPanel.GetComponentInChildren<TMP_Text>();
        msg.text = warn.message;
        warningPanel.SetActive(true);
        save.forDeletion = true;
    }

    public void CancelDelete() {
        slotFiles.ForEach(ss => ss.forDeletion = false);
        warningPanel.SetActive(false);
    }

    public void ConfirmDelete() {
        SaveSlot sc = slotFiles.Find(s => s.forDeletion == true);
        
        profile = loaded_profiles [sc.slotNumber - 1];
        
        rController.realmDB.Write(() => {
            rController.realmDB.Remove(profile);
        });

        Refresh();
        warningPanel.SetActive(false);
    }

    private void Refresh() {
        loaded_profiles = rController.Saves();
        InitializeSaveFiles();
        DisplayFiles();
    }
    private void DisplayFiles() {
        for (int x = 0; x < slotFiles.Count; x++) {
            Button[] btns = panels[x].GetComponentsInChildren<Button>();
            slotInformation[x].sltname.text = slotFiles[x].Name;
            
            if (slotFiles[x].Name == "No Data") {
                slotInformation[x].sltinfo.text = "No Data";
                slotInformation[x].sltimage.enabled = false;

                foreach(Button btn in btns) {
                    btn.interactable = false;
                }
            } else {
                slotInformation[x].sltinfo.text = $"Level : {slotFiles[x].Level}\n" +
                                                    $"Cells : {slotFiles[x].Cells:n0}\n" +
                                                    $"Genes : {slotFiles[x].Genes:n0}";
                slotInformation[x].sltimage.sprite = slotFiles[x].Sprite;
                slotInformation[x].sltimage.preserveAspect = true;
                slotInformation[x].sltimage.enabled = true;
                foreach(Button btn in btns) {
                    btn.interactable = true;
                }
            }
        }
    }

    private void InitializeSaveFiles() {
        for (int i = 0; i < slotFiles.Count; i++) {
            try {
                slotFiles[i].ID = loaded_profiles[i].Username_id;
                slotFiles[i].Name = loaded_profiles[i].Username;
                slotFiles[i].Gender = loaded_profiles[i].CharGender;
                slotFiles[i].Cells = loaded_profiles[i].CellCount;
                slotFiles[i].Genes = loaded_profiles[i].GeneCount;
                slotFiles[i].Level = loaded_profiles[i].Level;

                slotFiles[i].Sprite = (slotFiles[i].Gender == "Female") ? femaleSprite: maleSprite;

            } catch (Exception e) when (e is ArgumentOutOfRangeException || e is RealmInvalidObjectException) {
                slotFiles[i].Name = "No Data";
            }
        }
    }
}

[System.Serializable]
public class SlotInfo {
    public TMP_Text sltname;
    public TMP_Text sltinfo;
    public Image sltimage;
}
