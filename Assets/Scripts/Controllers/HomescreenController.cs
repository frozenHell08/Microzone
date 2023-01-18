using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomescreenController : MonoBehaviour
{
    [SerializeField] private RealmController rController;
    [SerializeField] private CurrentCharacter character;

    [SerializeField] private GameObject characterPanel;
    [SerializeField] private GameObject giftPanel;
    [SerializeField] private TMP_Text gendata_cells;
    [SerializeField] private TMP_Text gendata_genes;
    [SerializeField] private long cells;
    [SerializeField] private long genes;
    [SerializeField] private int bandaid;
    [SerializeField] private int milaon_s;

    void OnEnable() {
        if (character.firstLogin) {
            // -------------------------
            giftPanel.SetActive(true);

            character.firstLogin = false;
            character.cells += cells;
            character.genes += genes;
            character.healCount.Bandaid += bandaid;
            character.solutionsCount.milaon += milaon_s;

            rController.SyncToRealm();
        }

        Refresh();

        TMP_Text ch_name = characterPanel.GetComponentInChildren<TMP_Text>();
        // Image[] ch_sprite = characterPanel.GetComponentsInChildren<Image>();

        Image ch_sprite = Array.FindLast<Image>(characterPanel.GetComponentsInChildren<Image>(), 
            img => img.GetType() == typeof(Image));

        ch_sprite.preserveAspect = true;
        ch_sprite.sprite = character.sprite;
        ch_name.text = $"Welcome, {character.characterName}.";
    }

    public void AcceptGift() {
        giftPanel.SetActive(false);
        Refresh();
    }

    private void Refresh() {
        gendata_cells.text = $"{character.cells:n0}";
        gendata_genes.text = $"{character.genes:n0}";
    }
}

[System.Serializable]
public class yuu {

}