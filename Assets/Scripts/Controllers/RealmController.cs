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
    private RealmConfiguration config;

    Func<int, int, int> expCalculation = (ceilExp, experience) => ceilExp - experience;

    void OnEnable() {
        string exeRootFolder = Directory.GetCurrentDirectory();
        Directory.CreateDirectory(exeRootFolder + "/data");
        string realmPath = string.Format("{0}/data/profile.realm", exeRootFolder);
        config = new RealmConfiguration(realmPath);
        realmDB = Realm.GetInstance(config);
        Debug.Log("config made. realmcontroller");
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

    public void SyncGiftToRealm() {
        prof_model = FindProfile(character.characterID);

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

        // -------------------- STATS --------------------

        character.currentHealth = prof_model.CurrentHealth;
        character.maxHealth = prof_model.MaxHealth;
        character.experience = prof_model.Experience;
        character.barExperience = prof_model.CeilingExperience;
        character.totalExp = prof_model.TotalExperience;

        // -------------------- STAGES --------------------

        var slProperty = prof_model.Stages.GetType().GetProperties();
        int openStages = 0;

        foreach (var prop in slProperty) {
            var isOpen = prop.GetValue(prof_model.Stages);
            if (prop.Name.StartsWith("stage", StringComparison.OrdinalIgnoreCase) && (bool) isOpen){
               openStages++;
            }
        }

        character.totalStages = openStages;

        // -------------------- RESISTANCE --------------------

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

    public void SyncResToRealm() {
        prof_model = FindProfile(character.characterID);

        realmDB.Write(() => {
            prof_model.GeneCount = character.genes;

            prof_model.ImmuneSystem.BacteriaResist = character.res_bacteria ;
            prof_model.ImmuneSystem.ParasiteResist = character.res_parasite ;
            prof_model.ImmuneSystem.VirusResist = character.res_virus ; 
        });
    }
    
    public void SyncHealToRealm() {
        prof_model = FindProfile(character.characterID);

        realmDB.Write(() => {
            prof_model.CellCount = character.cells;

            prof_model.HealItems.Bandaid = healing.Bandaid;
            prof_model.HealItems.Injector = healing.Injector;
            prof_model.HealItems.Medicine = healing.Medicine;
        });
    }

    public void SyncSolnToRealm(string sol) {
        StringComparison oic = StringComparison.OrdinalIgnoreCase;
        prof_model = FindProfile(character.characterID);

        PropertyInfo prop = Array.Find<PropertyInfo>(prof_model.liquidMeds.GetType().GetProperties(), 
                    p => p.Name.Contains(sol, oic));

        FieldInfo field = Array.Find<FieldInfo>(soln.GetType().GetFields(),
                        f => f.Name.Equals(sol, oic));

        realmDB.Write(() => {
            prof_model.CellCount = character.cells;
            prop.SetValue(prof_model.liquidMeds, field.GetValue(soln));
        });
    }

    public void OpenNextLevel(string nextstage) {
        prof_model = FindProfile(character.characterID);

        PropertyInfo nextstagename = Array.Find<PropertyInfo>(prof_model.Stages.GetType().GetProperties(),
                    p => p.Name.Equals(nextstage));

        if (!(bool) nextstagename.GetValue(prof_model.Stages)) {
            realmDB.Write(() => {
                nextstagename.SetValue(prof_model.Stages, true);
            });

            character.totalStages += 1;
        }
    }

    public void UpdateHealthInRealm() {
        prof_model = FindProfile(character.characterID);

        realmDB.Write(() => {
            prof_model.CurrentHealth = character.currentHealth;
        });
    }

    public void UpdateItemInRealm(string name, int value) {
        prof_model = FindProfile(character.characterID);

        PropertyInfo prop = Array.Find<PropertyInfo>(prof_model.HealItems.GetType().GetProperties(),
                p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        realmDB.Write(() => {
            prop.SetValue(prof_model.HealItems, value);
        });
    }

    public void LevelUp() {
        prof_model = FindProfile(character.characterID);

        realmDB.Write(() => {
            prof_model.Experience = character.experience;
            prof_model.CeilingExperience = character.barExperience;
            prof_model.TotalExperience = character.totalExp;
            prof_model.Level = character.level;
        });
    }
}