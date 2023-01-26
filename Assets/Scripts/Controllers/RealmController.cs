using System.IO;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;
using Realms.Exceptions;
using TMPro;
using ProfileCreation;
using System;

public class RealmController : MonoBehaviour
{
    public Realm realmDB;
    [SerializeField] private CurrentCharacter character;
    [SerializeField] private CurrentHealing healing;
    [SerializeField] private CurrentSolution soln;
    private ProfileModel prof_model;
    private ResistancesModel res_model;
    private RealmConfiguration config;

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
                } catch (RealmDuplicatePrimaryKeyValueException) { }
            }
        }
    }

    public void SyncToRealm() {
        prof_model = FindProfile(character.characterID);
        // prof_model = FindProfile("JOSHUA_M");

        realmDB.Write(() => {
            prof_model.CellCount = character.cells;
            prof_model.GeneCount = character.genes;
            prof_model.firstLogin = character.firstLogin;
            prof_model.HealItems.Bandaid = character.healCount.Bandaid;
            prof_model.liquidMeds.Milaon_S = character.solutionsCount.milaon;
        });
    }

    public void SyncFromRealm() {
        prof_model = FindProfile(character.characterID);
        // prof_model = FindProfile("JOSHUA_M");

        // -------------------- RESISTANCE --------------------

        var slProperty = prof_model.Stages.GetType().GetProperties();
        int openStages = 0;

        foreach (var prop in slProperty) {
            var isOpen = prop.GetValue(prof_model.Stages);
            if (prop.Name.StartsWith("stage", StringComparison.OrdinalIgnoreCase) && (bool) isOpen){
               openStages++;
            }
        }

        character.totalStages = openStages;

        character.res_bacteria = prof_model.ImmuneSystem.BacteriaResist ;
        character.res_parasite = prof_model.ImmuneSystem.ParasiteResist ;
        character.res_virus = prof_model.ImmuneSystem.VirusResist ; 

        // -------------------- ITEMS --------------------

        foreach (FieldInfo f in healing.GetType().GetFields()) {
            PropertyInfo prop = Array.Find<PropertyInfo>(prof_model.HealItems.GetType().GetProperties(), 
                    p => p.Name.Equals(f.Name));

            f.SetValue(healing, prop.GetValue(prof_model.HealItems));
        }
        
        foreach (FieldInfo f in soln.GetType().GetFields()) {
            PropertyInfo prop = Array.Find<PropertyInfo>(prof_model.liquidMeds.GetType().GetProperties(), 
                    p => p.Name.StartsWith(f.Name, StringComparison.OrdinalIgnoreCase)); 

            f.SetValue(soln, prop.GetValue(prof_model.liquidMeds));
        }
    }
}