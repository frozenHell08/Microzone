using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProfileCreation;

public class StatInvInfo : MonoBehaviour
{
    #region
    [Header("Control")]
    [SerializeField] private RealmController rController;
    [SerializeField] private Sprite maleSprite;
    [SerializeField] private Sprite femaleSprite;

    [Header("Information")]
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text Status;
    [SerializeField] private Image characterSprite;
    [SerializeField] private TMP_Text Resistances;
    
    [Header("Inventory")]
    [SerializeField] private TMP_Text HealItems;
    [SerializeField] private TMP_Text SolutionArea;
    #endregion

    private ProfileModel profiledata;

    void OnEnable() {
        profiledata = rController.FindProfile(GeneralData.player_LoggedIn);
        
        characterName.text = profiledata.Username;
        ShowSprite(profiledata, characterSprite);
        ShowStatus(profiledata, Status);
        ShowResistances(profiledata, Resistances);
    }

    private void ShowResistances(ProfileModel profile, TMP_Text _resist) {
        int lvl = profile.Level;
        int rBact = profile.ImmuneSystem.BacteriaResist;
        int rPara = profile.ImmuneSystem.ParasiteResist;
        int rVir = profile.ImmuneSystem.VirusResist;

        int cBact = rBact * 3;
        int cPara = rPara * 3;
        int cVir = rVir * 3;
        int cLvl = lvl * 1;

        string resFor = $"<b>Bacteria</b> Lv.{rBact}: \t{cBact} (+{cLvl})\n" +
                        $"<b>Parasite</b> Lv.{rPara}:\t{cPara} (+{cLvl})\n" +
                        $"<b>Virus</b> Lv.{rVir}:\t{cVir} (+{cLvl})";

        _resist.text = resFor;
    }

    private void ShowStatus(ProfileModel profile, TMP_Text _statusArea) {
        string sCells = string.Format("{0:n0}", profile.CellCount);
        string sGenes = string.Format("{0:n0}", profile.GeneCount);
        
        var slProperty = profile.Stages.GetType().GetProperties();
        int i = 0;

        int totalOpen = 0;
        while (i < 18) {
            bool isOpen = (bool) slProperty[i].GetValue(profile.Stages);
            if (isOpen) {
                totalOpen += 1;
            }

            i++;
        }
        
        string y1 = $"<b>Gender</b> : \t{profile.CharGender}\n" +
                    $"<b>Level</b> : \t{profile.Level}\n" +
                    $"<b>Cells</b> : \t{sCells}\n" +
                    $"<b>Genes</b> : \t{sGenes}\n" +
                    $"<b>Stages</b> : \t{totalOpen}\n";

        _statusArea.text = y1;
    }

    private void ShowSprite(ProfileModel profile, Image img) {
        img.preserveAspect = true;

        switch(profile.CharGender) {
            case "Female":
                img.sprite = femaleSprite;
                break;
            case "Male":
                img.sprite = maleSprite;
                break;
        }
    }
}
