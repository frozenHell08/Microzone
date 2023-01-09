using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProfileCreation;

public class DeleteSave : MonoBehaviour
{
    [SerializeField] private RealmController rController;
    
    [Header("Details")]
    [SerializeField] private GameObject slot1;
    [SerializeField] private GameObject slot2;
    [SerializeField] private GameObject slot3;
    [SerializeField] private GameObject slot4;
    [SerializeField] private GameObject confirmation;
    [SerializeField] private GameObject refresh;

    private List<ProfileModel> loaded_profiles;
    private ProfileModel profile;
    
    public void attemptDeleteFile() {
        int deleteIndex = LoadGameFiles.trigerredGarbage;
        loaded_profiles = rController.Saves();
        profile = loaded_profiles[deleteIndex];

        rController.realmDB.Write(() => {
            rController.realmDB.Remove(profile);
        });

        refresh.SetActive(false);
        refresh.SetActive(true);
        confirmation.SetActive(false);
    }
}
