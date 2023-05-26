using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Compendium : MonoBehaviour
{

    [SerializeField] private CurrentCharacter ch;
    [SerializeField] private GameObject contentPanel;
    [SerializeField] private List<GameObject> infoPanels;
    [SerializeField] private List<Entry> infoEntry;
    // [SerializeField] private Image 

    void OnEnable() {
        Debug.Log("Open Compendium");

        // foreach (Transform child in contentPanel.transform) { //working
        //     Debug.Log(child.name);
        // }
        Transform childrenlist = contentPanel.transform;

        for (int l = 1; l <= ch.totalStages; l++) {
            Transform child = childrenlist.GetChild(l);
            Debug.Log($"stage : {l} --- enemy : {child}");

            
            GameObject childobject = child.gameObject;
            childobject.SetActive(true);

        }


        // foreach (Panel pnl in contentPanel.GetComponentsInChildren<Panel>(true)) {
        //     Debug.Log(pnl.Name);
        // }
    }

    public void SetData() {
        infoPanels.ForEach(item => {

        });
    }

}

#if UNITY_EDITOR
[ CustomEditor ( typeof(Compendium) ) ]
public class CompendiumEditor : Editor {
    // SerializedProperty types;

    void OnEnable() {
        // types = GetSeriaProperty("itemType");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Header("Source");
        Property("ch");
        Property("contentPanel");

        // EditorGUILayout.PropertyField(types, new GUIContent("Item Type"));
        // var typeindex = types.enumValueIndex;

        // switch (typeindex) {
        //     case 0 :
        //         Property("heal");
        //         break;
        //     case 1 :
        //         Property("solution");
        //         break;
        // }

        serializedObject.ApplyModifiedProperties();
    }

    private SerializedProperty GetSeriaProperty(string name) {
        return serializedObject.FindProperty(name);
    }

    private void Property(string name) {
        EditorGUILayout.PropertyField(serializedObject.FindProperty(name));
    }

    private void Header(string label) {
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
    }
}
#endif