using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProfileCreation;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelLogic : MonoBehaviour
{
    [SerializeField] private RealmController rController;
    [SerializeField] private CurrentCharacter ch;
    [SerializeField] [Range(1, 18)] private int _lvl;
    [SerializeField] private int TotalEnemies;
    [SerializeField] private GameObject completePanel;
    [SerializeField] private GameObject failedPanel;
    [SerializeField] private TMP_Text enemyTxtObj;
    [SerializeField] private Entry enemySource;
    [SerializeField] private Enemy enemyStats;
    [SerializeField] private GameObject charStats;
    [SerializeField] private TMP_Text enemyCounter;
    
    [SerializeField] private int cells;
    [SerializeField] private int experience;
    [SerializeField] private int milaon;
    [SerializeField] private int cinaon;
    [SerializeField] private int viraon;

    [SerializeField] private string map;
    
    private ProfileModel profiledata;
    private bool isDoneStage = false;
    private float messageDelay = 1f;

    Func<int, int, int> expCalculation = (experience, increment) => experience / increment;
    
    void OnEnable() {
        profiledata = rController.FindProfile(ch.characterID);
        LoadStageData();
        setFinishMessage(completePanel);
    }

    void Update() {
        if (isDoneStage) return;

        int enemyCount = CountEnemies();
        enemyCounter.text = enemyCount.ToString();

        if (enemyCount == 0) { //no more enemies in map
            StartCoroutine(ShowCompleteMessageWithDelay());
        }

        if (enemyCount > TotalEnemies) {
            StartCoroutine(ShowFailMessageWithDelay());
        }
    }

    private IEnumerator ShowCompleteMessageWithDelay() {
        isDoneStage = true;
        yield return new WaitForSeconds(messageDelay);
        completePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private IEnumerator ShowFailMessageWithDelay() {
        isDoneStage = true;
        yield return new WaitForSeconds(messageDelay);
        failedPanel.SetActive(true);
        Time.timeScale = 0f;
    } 

    public void LevelFinish() {
        string x = "stage" + _lvl;

        if (ch.totalStages == _lvl) {
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
        }

        // ----------------------------------

        rController.realmDB.Write(() => {
            profiledata.CellCount += cells;
            profiledata.TotalExperience += experience;
            profiledata.liquidMeds.Milaon_S += milaon;
            profiledata.liquidMeds.Cinaon_S += cinaon;
            profiledata.liquidMeds.Viraon_S += viraon;
        });

        ch.cells += cells;
        ch.totalExp += experience;
        ch.solutionsCount.milaon += milaon;
        ch.solutionsCount.cinaon += cinaon;
        ch.solutionsCount.viraon += viraon;

        // ----------------------------------

        ch.experience += experience;        

        while (ch.experience >= ch.barExperience) {
            ch.experience = ch.experience - ch.barExperience;

            ch.level += 1;
            ch.barExperience += 100;

            rController.LevelUp();
        }

        // ----------------------------------

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
        enemyTxtObj.text = enemySource.Species;

        foreach (TMP_Text txt in charStats.GetComponentsInChildren<TMP_Text>(true)) {
            if (txt.name.Contains("name")) txt.text = ch.characterName;
            if (txt.name.Contains("level")) txt.text = $"Lv. {ch.level.ToString()}";
            if (txt.name.Contains("exp")) txt.text = $"Exp\t{ch.experience.ToString()}/{ch.barExperience.ToString()}";
            if (txt.name.Contains("hp")) txt.text = $"HP\t{ch.currentHealth}/{ch.maxHealth}";
        }
    }

    public void FailedLevel() {
        ch.currentHealth = 1;
        rController.UpdateHealthInRealm();

        failedPanel.SetActive(true);
    }

    public void ReturnToMap() {
        SceneManager.LoadScene(map);
    }

    public Enemy GetEnemySource() {
        return enemyStats;
    }

    private int CountEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length;
    }

    public bool GetStageStatus() {
        return isDoneStage;
    }
}

#if UNITY_EDITOR
[ CustomEditor ( typeof(LevelLogic) ) ]
public class LevelLogicEditor : Editor {

    bool showRewards = false;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Header("Source", false);
        Property("rController");
        Property("ch");
        Property("enemySource");
        Property("enemyStats");
        Property("map");

        Header("Variables", true);
        Property("_lvl");
        Property("TotalEnemies");
        Property("enemyTxtObj");
        Property("charStats");
        Property("enemyCounter");

        Header("Message Panels", true);
        Property("completePanel");
        Property("failedPanel");

        EditorGUILayout.Space();
        showRewards = EditorGUILayout.BeginFoldoutHeaderGroup(showRewards, "Stage Rewards");

        if (showRewards) {
            Property("cells"); Property("experience"); Property("milaon");
            Property("cinaon"); Property("viraon");     
        }
        EditorGUILayout.EndFoldoutHeaderGroup();        

        serializedObject.ApplyModifiedProperties();
    }

    private SerializedProperty GetSeriaProperty(string name) {
        return serializedObject.FindProperty(name);
    }

    private void Property(string name) {
        EditorGUILayout.PropertyField(serializedObject.FindProperty(name));
    }

    private void Header(string label, bool space) {
        if (space) EditorGUILayout.Separator();
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
    }
}
#endif