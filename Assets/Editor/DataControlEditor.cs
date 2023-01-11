using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DataControl))]
public class DataControlEditor : Editor
{
    SerializedProperty listOfScreens;

    void OnEnable () {
        listOfScreens = serializedObject.FindProperty("_screens");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rController"));
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(listOfScreens, new GUIContent("Game Screen"));

        EditorGUILayout.Space();
        
        var another = listOfScreens.enumValueIndex;

        switch (another) {
            case 0 :
                DisplayShopInfo();
                break;
            case 1 :
                DisplayISInfo();
                break;
            case 2 :
                DisplayMapInfo();
                break;
        }

    serializedObject.ApplyModifiedProperties();
    }

    void DisplayShopInfo() {
        EditorGUILayout.LabelField("Currencies", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cells"));
    }

    void DisplayISInfo() {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("summary"));
        EditorGUILayout.LabelField("Currencies", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("genes"));
        EditorGUILayout.LabelField("Panels", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ISbact"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ISpara"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ISvir"));
        EditorGUILayout.LabelField("Open Upgrade Color", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("upgradeOpen"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("disabled"));
    }

    void DisplayMapInfo() {
        EditorGUILayout.LabelField("Currencies", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cells"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("genes"));
    }
}
