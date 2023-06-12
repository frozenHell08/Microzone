using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OverlaySolution : MonoBehaviour
{
    [SerializeField] private CurrentCharacter ch;
    [SerializeField] private Solution sol;

    private Image solSprite;
    private FieldInfo field;
    private int solNumber;
    private TMP_Text number;

    void OnEnable() {
        solSprite = gameObject.GetComponent<Image>();

        solSprite.preserveAspect = true;
        solSprite.sprite = sol.solutionSprite;

        Color newColor = solSprite.color;
        newColor.a = 0.5f;

        field = Array.Find<FieldInfo>(ch.solutionsCount.GetType().GetFields(),
                        f => f.Name.Equals(sol.solutionName, StringComparison.OrdinalIgnoreCase));
        
        number = gameObject.GetComponentsInChildren<TMP_Text>(true).FirstOrDefault(num => num.gameObject.name.Equals("number"));
        TMP_Text name = gameObject.GetComponentsInChildren<TMP_Text>(true).FirstOrDefault(name => name.gameObject.name.Equals("name"));
        solNumber = (int) field.GetValue(ch.solutionsCount);
        
        number.text = solNumber.ToString();
        name.text = sol.solutionName;

        if (solNumber == 0) {
            solSprite.color = newColor;
        }
    }

    void Update() {
        if (solNumber == 0) return;
        solNumber = (int) field.GetValue(ch.solutionsCount);
        number.text = solNumber.ToString();
    }
}
