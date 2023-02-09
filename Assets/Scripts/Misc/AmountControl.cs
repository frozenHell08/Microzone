using UnityEngine;
using TMPro;
using MadeEnums;

public class AmountControl : MonoBehaviour
{
    [SerializeField] private AmountButton buttonFunction;
    [SerializeField] private TMP_InputField amount;

    public void ChangeAmount() {
        int amtTxt = int.Parse(amount.text);
      
        switch(buttonFunction) {
            case AmountButton.Increase :
                amtTxt++;
                amount.text = amtTxt.ToString();
                break;
            case AmountButton.Decrease :
                if (amtTxt > 1) {
                    amtTxt--;
                    amount.text = amtTxt.ToString();
                }
                break;
        }
    }
}