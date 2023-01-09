using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProfileCreation;

public class LoadSelectedCharacter : MonoBehaviour
{
    [Header("Details")]
    [SerializeField] private RealmController rController;
    [Range(0, 3)]
    [SerializeField] private int saveIndex;

    [Header("Screens")]
    [SerializeField] private GameObject LoadScreen;
    [SerializeField] private GameObject HomeScreen;

    private List<ProfileModel> loaded_profiles;

    public void LoadSelected() {
        loaded_profiles = rController.Saves();
        GeneralData.player_LoggedIn = loaded_profiles[saveIndex].Username_id;
        LoadScreen.SetActive(false);
        HomeScreen.SetActive(true);
    }
}
