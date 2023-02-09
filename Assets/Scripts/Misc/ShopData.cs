using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using MadeEnums;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ShopData : MonoBehaviour
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private HealItem heal;
    [SerializeField] private Solution solution;

    void OnEnable() {
        switch (itemType) {
            case ItemType.Heal :
                DisplayData(heal);
                break;
            case ItemType.Solution : 
                DisplayData(null, solution);
                break;
        }
    }

    public HealItem AccessHeal() {
        return heal;
    }

    public Solution AccessSoln() {
        return solution;
    }

    private void DisplayData(HealItem h = null, Solution s = null) {
        StringComparison oic = StringComparison.OrdinalIgnoreCase;
        
        foreach (TMP_Text txtfield in this.GetComponentsInChildren<TMP_Text>(true)
            .Where(x => x.name.Contains("itemprev", oic)) ) {
            
            if (txtfield.name.Contains("name", oic)) {
                txtfield.text = (h != null) ? h.ItemName : s.solutionName ;
            }

            if (txtfield.name.Contains("desc", oic)) {
                txtfield.text = (h != null) ? h.defi : s.description ;
            }

            if (txtfield.name.Contains("price", oic)) {
                txtfield.text = (h != null) ? h.price.ToString() : s.price.ToString() ;
            }
        }

        foreach (Image img in this.GetComponentsInChildren<Image>(true)
            .Where(i => i.name.Contains("itemprev", oic))) {
                img.preserveAspect = true;
                img.sprite = (h != null) ? h.ItemSprite : s.solutionSprite ;
        }
    }
    
    #if UNITY_EDITOR
    void OnValidate() {
        switch (itemType) {
            case ItemType.Heal :
                DisplayData(heal);
                break;
            case ItemType.Solution : 
                DisplayData(null, solution);
                break;
        }
    }
    #endif
}

#if UNITY_EDITOR
[ CustomEditor ( typeof(ShopData) ) ]
public class ShopDataEditor : Editor {
    SerializedProperty types;

    void OnEnable() {
        types = GetSeriaProperty("itemType");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(types, new GUIContent("Item Type"));
        var typeindex = types.enumValueIndex;

        switch (typeindex) {
            case 0 :
                Property("heal");
                break;
            case 1 :
                Property("solution");
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }

    private SerializedProperty GetSeriaProperty(string name) {
        return serializedObject.FindProperty(name);
    }

    private void Property(string name) {
        EditorGUILayout.PropertyField(serializedObject.FindProperty(name));
    }
}
#endif