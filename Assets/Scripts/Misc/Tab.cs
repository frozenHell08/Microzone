using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Tab : MonoBehaviour
{
    [Header("Tab Customization")]
    [SerializeField] private Color tabShade = Color.gray ;
    [SerializeField] private Color tabTextShade = Color.white;
    [SerializeField] private float tabSpacing = 2f ;
    [Space]
    [SerializeField] private UIforTabButton[] uiForTab;
    [SerializeField] private GameObject[] tabContent;

    private Transform tabNameHeir, tabContentHeir;
    private Color active, inactive;
    private int numOfTabs, numOfContent;
    private int previous, current;

    void OnEnable() {
        tabNameHeir = transform.GetChild(0);
        tabContentHeir = transform.GetChild(1);
        numOfTabs = tabNameHeir.childCount;
        numOfContent = tabContentHeir.childCount;

        if (uiForTab.Length != numOfTabs || tabContent.Length != numOfContent) {
            Debug.LogError("Tab title and content are not the same amount.");
            return;
        }

        previous = current = 0;

        active = uiForTab[ 0 ].uiImage.color;
        inactive = uiForTab[ 1 ].uiImage.color;
    }

    public void OnTabClick() {
        GameObject clicked = EventSystem.current.currentSelectedGameObject;
        
        int clickIndex = Array.FindIndex(uiForTab, t => t.uiButton.name.Equals(clicked.name));

        if (current != clickIndex) {
            previous = current;
            current = clickIndex;

            tabContent[ previous ].SetActive(false);
            tabContent[ current ].SetActive(true);

            uiForTab[ previous ].uiImage.color = inactive;
            uiForTab[ current ].uiImage.color = active;

            uiForTab[ previous ].uiText.color = Light(tabTextShade, 0.45f);
            uiForTab[ current ].uiText.color = Dark(tabTextShade, 0.45f);
        }
    }
    
    #if UNITY_EDITOR
    void OnValidate() {
        tabNameHeir = transform.GetChild(0);
        tabContentHeir = transform.GetChild(1);
        numOfTabs = tabNameHeir.childCount;
        numOfContent = tabContentHeir.childCount;

        ChangeColor(tabShade);

        if (uiForTab.Length != numOfTabs || tabContent.Length != numOfContent) {
            Debug.LogError("Tab title and content are not the same amount.");
        }

        LayoutGroup layout = tabNameHeir.GetComponent<LayoutGroup>();
        ((HorizontalLayoutGroup) layout).spacing = tabSpacing;
    }
    #endif

    private void ChangeColor(Color color) {
        uiForTab[0].uiImage.color = color;
        Color inactiveTab = Dark(color, 0.3f);
        Color inactiveText = Light(tabTextShade, 0.45f);

        uiForTab[0].uiText.color = Dark(tabTextShade, 0.45f);

        for (int i = 1; i < numOfTabs; i++) {
            uiForTab[ i ].uiImage.color = inactiveTab;
            uiForTab[ i ].uiText.color = inactiveText;
        }
    }

    private Color Dark (Color color, float amount) {
        float h, s, v ;
        Color.RGBToHSV (color, out h, out s, out v) ;
        v = Mathf.Max (0f, v - amount) ;
        return Color.HSVToRGB (h, s, v) ;
    }

    private Color Light (Color color, float amount) {
        float h, s, v ;
        Color.RGBToHSV (color, out h, out s, out v) ;
        s = Mathf.Max (0f, s - amount);
        v = Mathf.Max (0f, v + amount) ;
        return Color.HSVToRGB (h, s, v) ;
    }
}

[System.Serializable]
public class UIforTabButton {
    public Button uiButton;
    public Image uiImage;
    public TMP_Text uiText; 
}