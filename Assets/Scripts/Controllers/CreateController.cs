using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using ProfileCreation;

public class CreateController : MonoBehaviour
{
    [SerializeField] private RealmController rController;
    [SerializeField] private CurrentCharacter character;
    [SerializeField] private Warning warning;
    [SerializeField] private GameObject genderPanel;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private GameObject homeScreen;
    [Header("Sprites")]
    [SerializeField] private Sprite msprite;
    [SerializeField] private Sprite fsprite;
    [Header("Colors")]
    [SerializeField] private Color original;
    [SerializeField] private Color dim;
    
    private ColorBlock current = ColorBlock.defaultColorBlock;
    private ColorBlock target = ColorBlock.defaultColorBlock;
    private ProfileModel profile;
    private string inputName;
    private TMP_Text msg;
    private TMP_InputField reflectedField;
    private Button[] genders;

    void OnEnable() { 
        msg = warningPanel.GetComponentInChildren<TMP_Text>();
        genders = genderPanel.GetComponentsInChildren<Button>();
        
        current.normalColor = original;
        target.normalColor = dim;

        Refresh();
    }

    public void Create() {
        string selectedGender = "";        
        float clrm = genders[0].colors.normalColor.a;
        float clrf = genders[1].colors.normalColor.a;

        character.Reset();

        if (inputName == null || inputName == "") warning.message = "No username entered.\n";
        
        if (clrm != clrf) {
            selectedGender = (clrm < clrf) ? "Female" : "Male";
            character.sprite = (selectedGender == "Female") ? fsprite : msprite;
        } else {
            warning.message += "No gender selected.";
        }

        if (warning.message != "") {
            msg.text = warning.message;
            warningPanel.SetActive(true);
            return;
        }

        character.characterID = $"{inputName.ToUpper()}_{selectedGender.Substring(0, 1)}";
        profile = rController.realmDB.Find<ProfileModel>(character.characterID);

        if (profile == null) {
            rController.GenerateProfile(character.characterID, inputName, selectedGender);
            
            Sync();
            homeScreen.SetActive(true);
            gameObject.SetActive(false);
        } else {
            warning.message = $"Character {inputName} ({selectedGender}) already exists.";
            msg.text = warning.message;
            warningPanel.SetActive(true);
        }
    }

    private void Sync() {
        profile = rController.realmDB.Find<ProfileModel>(character.characterID);

        character.characterName = profile.Username;
        character.firstLogin = profile.firstLogin;
        character.cells = profile.CellCount;
        character.genes = profile.GeneCount;
        character.level = profile.Level;
        character.gender = profile.CharGender;
        character.equippedSolution = null;
    }

    private void Refresh() {
        foreach (Button b in genders) {
            b.colors = current;
        }

        msg.text = "";
        warning.message = "";
        inputName = "";
        try {
            reflectedField.text = "";
        } catch (NullReferenceException) {};
    }

    public void Return() {
        Refresh();
        warningPanel.SetActive(false);
    }

    public void NameCheck() {
        TMP_InputField field = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>();
        inputName = field.text;
        reflectedField = field;
    }

    public void GenderClick() {
        GameObject genderClicked = EventSystem.current.currentSelectedGameObject;
        
        Button match = Array.Find (genders, n => n.name == genderClicked.name);

        switch(Array.IndexOf(genders, match)) {
            case 0 :
                genders[0].colors = current;
                genders[1].colors = target;
                break;
            case 1 :
                genders[0].colors = target;
                genders[1].colors = current;
                break;
        }
    }
}
