using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProfileCreation;

public class ISysControl : MonoBehaviour
{
    [Header("Control")]
    [SerializeField] private RealmController rController;

    [Header("Category")]
    [SerializeField] private Category category;
    [Range (1, 10)]
    [SerializeField] private int Level;

    [Header("Fields")]
    [SerializeField] private Image resImage;
    [SerializeField] private TMP_Text resName;
    [SerializeField] private TMP_Text resDesc;
    [SerializeField] private TMP_Text resPrice;

    [Header("References")]
    [SerializeField] private Sprite ibact;
    [SerializeField] private Sprite ipara;
    [SerializeField] private Sprite ivir;

    private ResistancesModel resModel;

    public void DisplayDetails() {
        resImage.preserveAspect = true;

        switch(category) {
            case Category.Bacteria:
                resImage.sprite = ibact;
                InfoSource(resModel, Level, "b");
                break;
            case Category.Parasite:
                resImage.sprite = ipara;
                InfoSource(resModel, Level, "p");
                break;
            case Category.Virus:
                resImage.sprite = ivir;
                InfoSource(resModel, Level, "v");
                break;
        }
    }

    private void InfoSource(ResistancesModel res, int _index, string _letter) {
        string _id = string.Concat(_letter, _index);

        resModel = rController.realmDB.Find<ResistancesModel>(_id);
        resName.text = resModel.Name;
        resDesc.text = resModel.Description;
        resPrice.text = resModel.Price.ToString();

        ConfirmPurchase.selectedItem = resModel;
    }
}

public enum Category { Bacteria, Parasite, Virus };