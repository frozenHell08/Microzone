using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharSelect : MonoBehaviour
{
    public Button button; //target button
    private ColorBlock target, original;

    public void changeNormalColor(float alpha) {
        target = button.colors;
        original = GetComponent<Button>().colors;

        target.normalColor = new Color(target.normalColor.r, target.normalColor.g, target.normalColor.b, alpha);
        button.colors = target;

        original.normalColor = new Color(original.normalColor.r, original.normalColor.g, original.normalColor.b, 1);
        gameObject.GetComponent<Button>().colors = original;
    }
}
