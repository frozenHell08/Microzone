using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadGameData : MonoBehaviour
{

    [SerializeField] private CurrentCharacter ch;
    [SerializeField] private TMP_Text gendata_cells;
    [SerializeField] private TMP_Text gendata_genes;

    [SerializeField] private GameObject chara_male;
    [SerializeField] private GameObject chara_female;

    void OnEnable() {
        gendata_cells.text = $"{ch.cells:n0}";
        gendata_genes.text = $"{ch.genes:n0}";

        if (ch.gender.Equals("Female")) {
            chara_female.SetActive(true);
        } else if (ch.gender.Equals("Male")) {
            chara_male.SetActive(true);
        }
    }
}
