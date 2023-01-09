using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Realms;
using TMPro;
using ProfileCreation;

public class LoadGameFiles : MonoBehaviour
{
    [SerializeField] private RealmController rController;
    private List<ProfileModel> loaded_profiles;
    
    [Header("Sprites")]
    [SerializeField] private Sprite msprite;
    [SerializeField] private Sprite fsprite;

    #region Save states
    [Header("Save Slot 1")]
    [SerializeField] private TMP_Text saveSlot1Name;
    [SerializeField] private TMP_Text saveSlot1Currency;
    [SerializeField] private Image saveSlot1Gender;

    [Header("Save Slot 2")]
    [SerializeField] private TMP_Text saveSlot2Name; 
    [SerializeField] private TMP_Text saveSlot2Currency;
    [SerializeField] private Image saveSlot2Gender;

    [Header("Save Slot 3")]
    [SerializeField] private TMP_Text saveSlot3Name;
    [SerializeField] private TMP_Text saveSlot3Currency;
    [SerializeField] private Image saveSlot3Gender;

    [Header("Save Slot 4")]
    [SerializeField] private TMP_Text saveSlot4Name;
    [SerializeField] private TMP_Text saveSlot4Currency;
    [SerializeField] private Image saveSlot4Gender;
    #endregion

    [Header("Garbage")]
    [SerializeField] private Button saveSlot1Garb;
    [SerializeField] private Button saveSlot2Garb;
    [SerializeField] private Button saveSlot3Garb;
    [SerializeField] private Button saveSlot4Garb;

    public static int trigerredGarbage;

    void OnEnable() {
        loaded_profiles = rController.Saves();

        UpdateScreen();        
    }

    public void UpdateScreen() {
        DisplaySave(0, saveSlot1Name, saveSlot1Currency, saveSlot1Gender, saveSlot1Garb);
        DisplaySave(1, saveSlot2Name, saveSlot2Currency, saveSlot2Gender, saveSlot2Garb);
        DisplaySave(2, saveSlot3Name, saveSlot3Currency, saveSlot3Gender, saveSlot3Garb);
        DisplaySave(3, saveSlot4Name, saveSlot4Currency, saveSlot4Gender, saveSlot4Garb);
    }
 
    public void DisplaySave(int saveIndex, TMP_Text name, TMP_Text currency, Image gender, Button _garbage) {
        try {
            ProfileModel loaded_file = loaded_profiles[saveIndex];
            string dispCurrency = string.Format("Cells : {0}\nGenes : {1}", loaded_file.CellCount, loaded_file.GeneCount);
            
            name.text = loaded_file.Username;
            currency.text = dispCurrency;
            gender.preserveAspect = true;
            gender.enabled = true;
            NoData(_garbage, true);

            switch (loaded_file.CharGender) {
                case "Female" :
                    gender.sprite  = fsprite;
                    break;
                case "Male" :
                    gender.sprite = msprite;
                    break;
            }
        } catch (ArgumentOutOfRangeException e) {
            name.text = "No Data";
            currency.text = "No Data";
            gender.enabled = false;
            NoData(_garbage, false);
            Debug.Log(e);
        }
    }

    private void NoData (Button btn, bool _value) {
        Button parent = btn.transform.parent.GetComponent<Button>();

        btn.interactable = _value;
        parent.interactable = _value;
    }
}
