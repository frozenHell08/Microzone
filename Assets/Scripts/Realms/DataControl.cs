using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProfileCreation;

public class DataControl : MonoBehaviour
{
    [Header("Control")]
    [SerializeField] private RealmController rController;

    [SerializeField] private TMP_Text cells;
    [SerializeField] private TMP_Text genes;
    [SerializeField] private TMP_Text summary;
    [SerializeField] private GameObject ISbact;
    [SerializeField] private GameObject ISpara;
    [SerializeField] private GameObject ISvir;
    [SerializeField] private Color upgradeOpen;
    [SerializeField] private Color disabled;
    [SerializeField] private ScreensList _screens;
    private ProfileModel profiledata;
    
    void OnEnable() {
        profiledata = rController.FindProfile(GeneralData.player_LoggedIn);
        // profiledata = rController.FindProfile("JOSHUA_M");
        switch (_screens) {
            case ScreensList.Shop :
                cells.text = string.Format("{0:n0}", profiledata.CellCount);
                break;
            case ScreensList.ImmuneSystem :
                genes.text = string.Format("{0:n0}", profiledata.GeneCount);
                ISSummary(profiledata);
                LoadResistances(profiledata);
                break;
            case ScreensList.Map:
                cells.text = string.Format("{0:n0}", profiledata.CellCount);
                genes.text = string.Format("{0:n0}", profiledata.GeneCount);
                break;
        }
        
    }

    void OnDisable() {
        ResetBoard(ISbact, ISpara, ISvir);
    }

    public void LoadResistances(ProfileModel profile) {
        int lBact = profile.ImmuneSystem.BacteriaResist;
        int lPara = profile.ImmuneSystem.ParasiteResist;
        int lVir = profile.ImmuneSystem.VirusResist;

        DataSource(ISbact, lBact);
        DataSource(ISpara, lPara);
        DataSource(ISvir, lVir);
    }

    private void DataSource(GameObject _panel, int reslvl) {
        ColorBlock target;

        if (reslvl == 10) {
            for (int i = 0; i < reslvl; i++) {
                Button btn = _panel.transform.GetChild(i).gameObject.GetComponent<Button>();
                target = btn.colors;
                
                target.disabledColor = new Color(1, 1, 1, 1);
                btn.colors = target;
                btn.interactable = false;
            }
        } else {
            for (int i = 0; i <= reslvl; i++) {
                Button btn = _panel.transform.GetChild(i).gameObject.GetComponent<Button>();
                target = btn.colors;
                
                if (i < reslvl) {
                    target.disabledColor = new Color(1, 1, 1, 1);
                    btn.colors = target;
                    btn.interactable = false;
                } else if (i == reslvl) {
                    target.normalColor = upgradeOpen;
                    btn.colors = target;
                    btn.interactable = true;
                }
            }
        }
    }

    private void ResetBoard(GameObject _panel1, GameObject _panel2, GameObject _panel3) {
        ColorBlock target;
        Button btn, btn2, btn3;

        for (int i = 0; i < 10; i++ ) {
            btn = _panel1.transform.GetChild(i).gameObject.GetComponent<Button>();
            btn2 = _panel2.transform.GetChild(i).gameObject.GetComponent<Button>();
            btn3 = _panel3.transform.GetChild(i).gameObject.GetComponent<Button>();

            target = btn.colors;

            target.normalColor = disabled;
            btn.colors = target;
            btn2.colors = target;
            btn3.colors = target;

            btn.interactable = false;
            btn2.interactable = false;
            btn3.interactable = false;
        }
    }

    private void ISSummary(ProfileModel profile) {
        int rBact = profile.ImmuneSystem.BacteriaResist;
        int rPara = profile.ImmuneSystem.ParasiteResist;
        int rVir = profile.ImmuneSystem.VirusResist;

        int cBact = rBact * 3;
        int cPara = rPara * 3;
        int cVir = rVir * 3;

        string resFor = $"<b>Bacteria</b> Lv.{rBact}: \t{cBact}\n" +
                        $"<b>Parasite</b> Lv.{rPara}: \t{cPara}\n" +
                        $"<b>Virus</b> Lv.{rVir}: \t\t{cVir}";

        summary.text = resFor;
    }
}

public enum ScreensList { Shop, ImmuneSystem, Map };
