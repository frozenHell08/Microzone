using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProfileCreation;
using UnityEngine.SceneManagement;

public class LevelLogic : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private RealmController rController;
    [SerializeField] [Range(1, 18)] private int _lvl;
    [SerializeField] private int TotalEnemies;
    [SerializeField] private GameObject panel;
    
    [Header("Rewards")]
    [SerializeField] private int cells;
    [SerializeField] private int milaon;

    [Header("Scene")]
    [SerializeField] private Object map;
    
    private ProfileModel profiledata;
    private int killedEnemies = 0;
    
    void OnEnable() {
        profiledata = rController.FindProfile(GeneralData.player_LoggedIn);
        // profiledata = rController.FindProfile("JOSHUA_M");
    }

    public void AddKill() {
        if (killedEnemies < TotalEnemies) {
            killedEnemies++;

            if (killedEnemies == TotalEnemies) {
                panel.SetActive(true);
            }
        }
    }

    public void LevelFinish() {
        string x = "stage" + _lvl;

        rController.realmDB.Write(() => {
            if (x == "stage1") {
                profiledata.Stages.stage2 = true;
            } else if (x == "stage2") {
                profiledata.Stages.stage3 = true;
            } else if (x == "stage3") {
                profiledata.Stages.stage4 = true;
            } else if (x == "stage4") {
                profiledata.Stages.stage5 = true;
            } else if (x == "stage5") {
                profiledata.Stages.stage6 = true;
            } else if (x == "stage6") {
                profiledata.Stages.stage7 = true;
            } else if (x == "stage7") {
                profiledata.Stages.stage8 = true;
            } else if (x == "stage8") {
                profiledata.Stages.stage9 = true;
            } else if (x == "stage9") {
                profiledata.Stages.stage10 = true;
            } else if (x == "stage10") {
                profiledata.Stages.stage11 = true;
            } else if (x == "stage11") {
                profiledata.Stages.stage12 = true;
            } else if (x == "stage12") {
                profiledata.Stages.stage13 = true;
            } else if (x == "stage13") {
                profiledata.Stages.stage14 = true;
            } else if (x == "stage14") {
                profiledata.Stages.stage15 = true;
            } else if (x == "stage15") {
                profiledata.Stages.stage16 = true;
            } else if (x == "stage16") {
                profiledata.Stages.stage17 = true;
            } else if (x == "stage17") {
                profiledata.Stages.stage18 = true;
            }
        });

        // ----------------------------------

        rController.realmDB.Write(() => {
            profiledata.CellCount += cells;
            profiledata.liquidMeds.Milaon_S += milaon;
        });

        SceneManager.LoadScene(map.name);

        // ----------------------------------
    }
}
