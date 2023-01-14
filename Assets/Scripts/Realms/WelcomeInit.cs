using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProfileCreation;

public class WelcomeInit : MonoBehaviour
{
    public RealmController rController;
    public WarningSO warning;
    private List<ProfileModel> loaded_profiles;
    public Button newGame, loadGame;

    void OnEnable() {
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
