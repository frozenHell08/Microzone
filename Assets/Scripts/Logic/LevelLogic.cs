using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProfileCreation;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLogic : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private RealmController rController;
    [SerializeField] private CurrentCharacter ch;
    [SerializeField] [Range(1, 18)] private int _lvl;
    [SerializeField] private int TotalEnemies;
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text enemyTxtObj;
    [SerializeField] private String enemyName;
    [SerializeField] private GameObject charStats;
    
    [Header("Rewards")]
    [SerializeField] private int cells;
    [SerializeField] private int experience;
    [SerializeField] private int milaon;
    [SerializeField] private int cinaon;
    [SerializeField] private int viraon;

    [Header("Scene")]
    [SerializeField] private string map;
    
    private ProfileModel profiledata;
    private int killedEnemies = 0;

    Func<int, int, int> expCalculation = (experience, increment) => experience / increment;
    
    void OnEnable() {
        profiledata = rController.FindProfile(ch.characterID);
        // profiledata = rController.FindProfile("JOSHUA_M");
        LoadStageData();
        setFinishMessage(panel);
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

        if (x.Equals("stage1")) {
            rController.OpenNextLevel("stage2");
        } else if (x == "stage2") {
            rController.OpenNextLevel("stage3");
        } else if (x == "stage3") {
            rController.OpenNextLevel("stage4");
        } else if (x == "stage4") {
            rController.OpenNextLevel("stage5");
        } else if (x == "stage5") {
            rController.OpenNextLevel("stage6");
        } else if (x == "stage6") {
            rController.OpenNextLevel("stage7");
        } else if (x == "stage7") {
            rController.OpenNextLevel("stage8");
        } else if (x == "stage8") {
            rController.OpenNextLevel("stage9");
        } else if (x == "stage9") {
            rController.OpenNextLevel("stage10");
        } else if (x == "stage10") {
            rController.OpenNextLevel("stage11");
        } else if (x == "stage11") {
            rController.OpenNextLevel("stage12");
        } else if (x == "stage12") {
            rController.OpenNextLevel("stage13");
        } else if (x == "stage13") {
            rController.OpenNextLevel("stage14");
        } else if (x == "stage14") {
            rController.OpenNextLevel("stage15");
        } else if (x == "stage15") {
            rController.OpenNextLevel("stage16");
        } else if (x == "stage16") {
            rController.OpenNextLevel("stage17");
        } else if (x == "stage17") {
            rController.OpenNextLevel("stage18");
        }

        // ----------------------------------

        rController.realmDB.Write(() => {
            profiledata.CellCount += cells;
            profiledata.Experience += experience;
            profiledata.liquidMeds.Milaon_S += milaon;
            profiledata.liquidMeds.Cinaon_S += cinaon;
            profiledata.liquidMeds.Viraon_S += viraon;
        });

        ch.cells += cells;
        ch.experience += experience;
        ch.solutionsCount.milaon += milaon;
        ch.solutionsCount.cinaon += cinaon;
        ch.solutionsCount.viraon += viraon;

        Debug.Log($"exp : {ch.experience}");
        Debug.Log($"level : {ch.level}");

        int result = expCalculation(ch.experience, 100);
        Debug.Log($"result : {result}");

        if (ch.level < result) {
            rController.realmDB.Write(() => {
                profiledata.Level += 1;
            });

            ch.level += 1;
            Debug.Log("level up");
        } else {
            Debug.Log("no level up");
        }

        SceneManager.LoadScene(map);

        // ----------------------------------
    }

    private void setFinishMessage(GameObject finishPanel) {
        TMP_Text msg = finishPanel.GetComponentInChildren<TMP_Text>(true);

        string fm = $"Stage Completed !\n\n" +
                    $"Rewards : \n" +
                    $"{experience} Exp \n" +
                    $"Cells x{cells}\n" +
                    $"Milaon x{milaon}\n";
        
        if (cinaon > 0) {
            fm += $"Cinaon x{cinaon}\n";
        }

        if (viraon > 0) {
            fm += $"Viraon x{viraon}";
        }

        msg.text = fm;
    }

    private void LoadStageData() {
        enemyTxtObj.text = enemyName;

        foreach (TMP_Text txt in charStats.GetComponentsInChildren<TMP_Text>(true)) {
            if (txt.name.Contains("name")) txt.text = ch.characterName;
            if (txt.name.Contains("level")) txt.text = ch.level.ToString();
            if (txt.name.Contains("exp")) txt.text = ch.experience.ToString();
            if (txt.name.Contains("hp")) txt.text = $"{ch.currentHealth}/{ch.maxHealth}";
        }
    }
}
