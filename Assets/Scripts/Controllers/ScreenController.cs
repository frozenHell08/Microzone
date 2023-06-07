using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ProfileCreation;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ScreenController : MonoBehaviour
{
    [SerializeField] private RealmController rController;
    [SerializeField] private CurrentCharacter character;
    [SerializeField] private Static isReturning;
    [SerializeField] private GameObject welcomeScreen;
    [SerializeField] private GameObject homeScreen;
    [SerializeField] private GameObject characterCreate;
    [SerializeField] private GameObject loadCharacter;
    [SerializeField] private GameObject statusInventory;
    [SerializeField] private GameObject compendium;
    [SerializeField] private GameObject minigame;
    [SerializeField] private GameObject immuneSystem;
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject logout;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private Button newGame;
    [SerializeField] private Button loadGame;
    [SerializeField] private Scrollbar scrollbar;
    
    private List<ProfileModel> loaded_profiles;

    void Awake() {
        if (isReturning.boolValue) {
            welcomeScreen.SetActive(false);
            homeScreen.SetActive(true);
        }
    }

    void Update() {
        if (welcomeScreen.activeSelf) {
            loaded_profiles = rController.Saves();

            if (loaded_profiles.Count == 0) { //no saves
                loadGame.interactable = false;
            } else if (loaded_profiles.Count == 4) { // max saves (4)
                newGame.interactable = false;
                loadGame.interactable = true;
            } else if (loaded_profiles.Count > 0 && loaded_profiles.Count < 4){
                loadGame.interactable = true;
                newGame.interactable = true;
            }
        }
    }

    public void OpenSettings() {
        settings.SetActive(true);
        homeScreen.SetActive(false);
    }

    public void CloseSettings() {
        settings.SetActive(false);
        homeScreen.SetActive(true);
    }
    
    public void OpenShop() {
        shop.SetActive(true);
        homeScreen.SetActive(false);
    }
    
    public void CloseShop() {
        shop.SetActive(false);
        homeScreen.SetActive(true);
    }
    
    public void OpenISys() {
        immuneSystem.SetActive(true);
        homeScreen.SetActive(false);
    }
    
    public void CloseISys() {
        immuneSystem.SetActive(false);
        homeScreen.SetActive(true);
    }
    
    public void OpenMinigame() {
        minigame.SetActive(true);
        homeScreen.SetActive(false);
    }
    
    public void CloseMinigame() {
        minigame.SetActive(false);
        homeScreen.SetActive(true);
    }

    public void OpenCompendium() {
        compendium.SetActive(true);
    }

    public void CloseCompendium() {
        compendium.SetActive(false);
    }

    public void OpenSInv() {
        statusInventory.SetActive(true);
    }

    public void CloseSInv() {
        statusInventory.SetActive(false);
    }
    
    public void ToMap(string map) {
        homeScreen.SetActive(false);
        SceneManager.LoadScene(map);
    }

    public void Logout() {
        homeScreen.SetActive(false);
        welcomeScreen.SetActive(true);
        isReturning.boolValue = false;
        character.Reset();
    }

    public void NewCharacter() {
        welcomeScreen.SetActive(false);
        characterCreate.SetActive(true);
    }

    public void BackNewChara() {
        welcomeScreen.SetActive(true);
        characterCreate.SetActive(false);
    }

    public void LoadCharacter() {
        welcomeScreen.SetActive(false);
        loadCharacter.SetActive(true);
    }

    public void BackLoadCharacter() {
        welcomeScreen.SetActive(true);
        loadCharacter.SetActive(false);
    }

    public void ConfirmQuit(Warning msg) {
        TMP_Text message = messagePanel.GetComponentsInChildren<TMP_Text>(true).FirstOrDefault(txt => txt.name.Equals("confirmMessage"));

        message.text = msg.message;
        messagePanel.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void OpenTutorial() {
        tutorialPanel.SetActive(true);
        scrollbar.value = 1f;
    }

    public void ExitTutorial() {
        tutorialPanel.SetActive(false);
    }
}

#if UNITY_EDITOR
[ CustomEditor ( typeof(ScreenController) ) ]
public class ScreenControllerEditor : Editor {

    bool showScreens, showGameInterface, showButtons = false;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Header("Source");
        Property("rController");
        Property("character");
        Property("isReturning");
        Property("messagePanel");
        Property("scrollbar");

        EditorGUILayout.Space();
        showScreens = EditorGUILayout.BeginFoldoutHeaderGroup(showScreens, "Intro Screens");
        
        if (showScreens) {
            Property("welcomeScreen");
            Property("characterCreate");
            Property("loadCharacter");
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space();
        showGameInterface = EditorGUILayout.BeginFoldoutHeaderGroup(showGameInterface, "In-game Screens");

        if (showGameInterface) {
            Property("homeScreen");
            Property("statusInventory");
            Property("compendium");
            Property("minigame");
            Property("immuneSystem");
            Property("shop");
            Property("settings");
            Property("tutorialPanel");
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space();
        showButtons = EditorGUILayout.BeginFoldoutHeaderGroup(showButtons, "Title Buttons");

        if (showButtons) {
            Property("newGame");
            Property("loadGame");
        }

        serializedObject.ApplyModifiedProperties();
    }

    private SerializedProperty GetSeriaProperty(string name) {
        return serializedObject.FindProperty(name);
    }

    private void Property(string name) {
        EditorGUILayout.PropertyField(serializedObject.FindProperty(name));
    }

    private void Header(string label) {
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
    }
}
#endif