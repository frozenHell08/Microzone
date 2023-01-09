using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using ProfileCreation;

public class CharSel_confirm : MonoBehaviour
{
    private ProfileModel charCheck;
    [SerializeField] private TMP_Text message;
    [SerializeField] private RealmController ctrl;
    
    [Header("Objects")]
    [SerializeField] private Button msprite;
    [SerializeField] private Button fsprite;
    [SerializeField] private GameObject warning;

    [Header("Screens")]
    [SerializeField] private GameObject CharSelect;
    [SerializeField] private GameObject HomeScreen;

    public void confirm (TMP_InputField username) {
        string user_name = username.text;
        string selectedGender = "";
        string userwarn = "";
        string genderwarn = "";
        int warncount = 0;

        if (user_name == "") {
            userwarn = "No username entered.\n";
            warncount++;
        }

        //character selection
        float clrm = msprite.colors.normalColor.a;
        float clrf = fsprite.colors.normalColor.a;

        //if both 1, show warning to choose a gender.
        if (clrm != clrf) {
            selectedGender = (clrm < clrf) ? "Female" : "Male";
        } else {
            warncount++;
            genderwarn = "No gender selected.";
        }

        if (warncount > 0) {
            message.text = string.Format("{0} {1}", userwarn, genderwarn);
            warning.SetActive(true);
            return;
        }

        string id_make = string.Format("{0}_{1}", user_name.ToUpper(), selectedGender.Substring(0, 1));

        charCheck = ctrl.realmDB.Find<ProfileModel>(id_make);

        if (charCheck == null) {
            ctrl.GenerateProfile(id_make, user_name, selectedGender);
            returnToDefault(username);
            changeToHomescreen();
        } else {
            message.text = string.Format("Character {0} ({1}) already exists.", user_name, selectedGender);
            warning.SetActive(true);
            returnToDefault(username);
        }
    }

    private void changeToHomescreen() {
        CharSelect.SetActive(false);
        HomeScreen.SetActive(true);
    }

    private void returnToDefault(TMP_InputField _username) {
        ColorBlock defaultColor = msprite.colors;

        defaultColor.normalColor = new Color(defaultColor.normalColor.r, defaultColor.normalColor.g, defaultColor.normalColor.b, 1);
        msprite.colors = defaultColor;
        fsprite.colors = defaultColor;
        _username.text = "";
    }
}
