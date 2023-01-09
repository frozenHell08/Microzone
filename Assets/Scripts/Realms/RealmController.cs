using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;
using Realms.Exceptions;
using TMPro;
using ProfileCreation;

public class RealmController : MonoBehaviour
{
    public Realm realmDB;
    private ProfileModel prof_model;
    private ResistancesModel res_model;
    public static RealmConfiguration config;

    void OnEnable() {
        string exeRootFolder = Directory.GetCurrentDirectory();
        Directory.CreateDirectory(exeRootFolder + "/data");
        string realmPath = string.Format("{0}/data/profile.realm", exeRootFolder);
        config = new RealmConfiguration(realmPath);
        realmDB = Realm.GetInstance(config);
        Debug.Log("config made. realmcontroller");

        ResistanceData();
    }

    void OnDisable() {
        Debug.Log("disposed. realmcontroller");
        realmDB.Dispose();
    }

    public void GenerateProfile(string id, string username, string gender) {
        prof_model = realmDB.Find<ProfileModel>(id);

        realmDB.Write(() => {
            prof_model = realmDB.Add(new ProfileModel(
                id,
                username,
                gender
            ));
        });
        
        GeneralData.player_LoggedIn = id;
    }

    public ProfileModel FindProfile(string charID) {
        prof_model = realmDB.Find<ProfileModel>(charID);
        return prof_model;
    }

    public List<ProfileModel> Saves() {
        List<ProfileModel> loaded_profiles = new List<ProfileModel>();

        foreach (var files in realmDB.All<ProfileModel>()) {
            loaded_profiles.Add(files);
        };

        return loaded_profiles;
    }

    public void ResistanceData() {
        ISData isd = new ISData();
        List<ResistancesModel> resData = isd.InitializeData();
        int c = 0;

        foreach (var counting in realmDB.All<ResistancesModel>()) {
            c++;
        }

        if (c < 30) {
            foreach (var data in resData) {
                try {
                    realmDB.Write(() => {
                        res_model = realmDB.Add(data);
                    });
                } catch (RealmDuplicatePrimaryKeyValueException e) {} //CS0168 #pragma warning disable
           
            }
        }
    }
}
