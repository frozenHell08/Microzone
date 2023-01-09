using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProfileCreation;

public class GeneralData : MonoBehaviour
{
    public RealmController rController;
    private ProfileModel profiledata;
    [SerializeField] private TMP_Text welcomePlayer, gendata_cells, gendata_genes;

    public static string player_LoggedIn;
    public GameObject welcomeGift;
    public Image image;
    public Sprite maleSprite, femaleSprite;
    public Button sprite, play, mgame, IS, shop, settings, logout;

    void OnEnable() {
        profiledata = rController.FindProfile(player_LoggedIn);
       
        if (profiledata.firstLogin) {
            play.interactable = false;
            mgame.interactable = false;
            IS.interactable = false;
            shop.interactable = false;
            settings.interactable = false;
            logout.interactable = false;
            sprite.interactable = false;
            welcomeGift.SetActive(true);
        }

        welcomePlayer.text = string.Format("Welcome, {0}", profiledata.Username);
        image.preserveAspect = true;
        
        switch (profiledata.CharGender) {
            case "Female" :
                image.sprite  = femaleSprite;
                break;
            case "Male" :
                image.sprite = maleSprite;
                break;
        }

        gendata_cells.text = string.Format("{0:n0}", profiledata.CellCount);
        gendata_genes.text = string.Format("{0:n0}", profiledata.GeneCount);
    }
}
