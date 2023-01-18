using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ProfileCreation;

public class ScreenController : MonoBehaviour
{
    [Header("Controls")]
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
    [SerializeField] private Button newGame;
    [SerializeField] private Button loadGame;
    private List<ProfileModel> loaded_profiles;

    void Awake() {
        if (isReturning.boolValue) {
            welcomeScreen.SetActive(false);
            homeScreen.SetActive(true);
        }
    }

    void OnEnable() {
        
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
    }
    
    public void CloseShop() {
        shop.SetActive(false);
    }
    
    public void OpenISys() {
        immuneSystem.SetActive(true);
    }
    
    public void CloseISys() {
        immuneSystem.SetActive(false);
    }
    
    public void OpenMinigame() {
        minigame.SetActive(true);
    }
    
    public void CloseMinigame() {
        minigame.SetActive(false);
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

    public void QuitGame() {
        Application.Quit();
    }
}
