using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Realms;
using TMPro;
using ProfileCreation;

public class StarterPack : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField] private RealmController rController;
    
    [Header("Data")]
    [SerializeField] private int cells;
    [SerializeField] private int genes;
    [SerializeField] private int bandaid;
    [SerializeField] private int milaonSoln;

    [Header("Fields")]
    [SerializeField] private TMP_Text gendata_cells;
    [SerializeField] private TMP_Text gendata_genes;

    [Header("Buttons")]
    [SerializeField] private Button play;
    [SerializeField] private Button sprite;
    [SerializeField] private Button mgame;
    [SerializeField] private Button IS;
    [SerializeField] private Button shop;
    [SerializeField] private Button settings;
    [SerializeField] private Button logout;

    [Header("Screen")]
    [SerializeField] private GameObject StarterPackScreen;
    private ProfileModel profiledata;

    void OnEnable() {
        profiledata = rController.FindProfile(GeneralData.player_LoggedIn);
    }

    public void acceptGift() {
        rController.realmDB.Write(() => {
           profiledata.firstLogin = false;
           profiledata.CellCount += cells; 
           profiledata.GeneCount += genes;
           profiledata.HealItems.Bandaid += bandaid; 
           profiledata.liquidMeds.Milaon_S += milaonSoln;
        });

        gendata_cells.text = string.Format("{0:n0}", profiledata.CellCount);
        gendata_genes.text = string.Format("{0:n0}", profiledata.GeneCount);

        StarterPackScreen.SetActive(false);
        sprite.interactable = true;
        play.interactable = true;
        mgame.interactable = true;
        IS.interactable = true;
        shop.interactable = true;
        settings.interactable = true;
        logout.interactable = true;
    }
}
