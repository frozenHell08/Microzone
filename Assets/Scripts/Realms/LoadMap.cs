using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProfileCreation;

public class LoadMap : MonoBehaviour
{
    [Header("Control")]
    [SerializeField] private RealmController rController;
    [SerializeField] private GameObject Map;

    private ProfileModel profiledata;

    void OnEnable() {
        string character = GeneralData.player_LoggedIn;

        profiledata = rController.FindProfile(character);
        Debug.Log(Screen.currentResolution);
        ifOpen(profiledata, Map);
    }

    private void ifOpen(ProfileModel profile, GameObject _map) {
        var slProperty = profile.Stages.GetType().GetProperties();
        int i = 0;

        foreach (Transform child in _map.transform) {
            try {
                bool isOpen = (bool) slProperty[i].GetValue(profile.Stages);

                if (isOpen) {
                    child.transform.GetChild(0).gameObject.SetActive(false);
                }

                i++;
            } catch (InvalidCastException e) {
                Debug.LogException(e);
            }
        }
    }
}
