using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmountControl : MonoBehaviour
{
    [SerializeField] private AmountButton buttonFunction;
    [SerializeField] private TMP_InputField amount;

    public void ChangeAmount() {
        int amtTxt;

        if (amount.text == "") {
            amtTxt = 0;
        } else {
            amtTxt = int.Parse(amount.text);
        }
      
        switch(buttonFunction) {
            case AmountButton.Increase :
                amtTxt++;
                amount.text = amtTxt.ToString();
                break;
            case AmountButton.Decrease :
                if (amtTxt > 0) {
                    amtTxt--;
                    amount.text = amtTxt.ToString();
                }
                break;
        }
    }
}

public enum AmountButton { Increase, Decrease }
