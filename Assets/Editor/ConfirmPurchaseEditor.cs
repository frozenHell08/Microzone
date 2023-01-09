using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ConfirmPurchase))]
public class ConfirmPurchaseEditor : Editor
{
    SerializedProperty pScreens;
    SerializedProperty tabs;

    void OnEnable() {
        pScreens = serializedObject.FindProperty("_purchasing");
        tabs = serializedObject.FindProperty("_tab");
    }

    public override void OnInspectorGUI() {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rController"));
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(pScreens, new GUIContent("Purchase Screen"));

        EditorGUILayout.Space();

        var another = pScreens.enumValueIndex;

        switch (another) {
            case 0 :
                DisplayShopInfo();
                break;
            case 1 :
                DisplayISInfo();
                break;
        }
        serializedObject.ApplyModifiedProperties();
    }

    void DisplayShopInfo() {
        EditorGUILayout.PropertyField(tabs, new GUIContent("Shop Tab"));
        var itemControl = tabs.enumValueIndex;

        switch(itemControl) {
            case 0 :
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_item"), new GUIContent("Item"));
                break;
            case 1 :
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_soln"), new GUIContent("Solution"));
                break;
        }
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("warningShop"));
        EditorGUILayout.LabelField("Currencies", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cells"));
        EditorGUILayout.LabelField("Price", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("itemPrice"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("amount"));
    }

    void DisplayISInfo() {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("summary"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("warningIS"));
        EditorGUILayout.LabelField("Currencies", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("genes"));
        EditorGUILayout.LabelField("Price", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("upgradePrice"));
        EditorGUILayout.LabelField("Panels", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ISbact"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ISpara"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ISvir"));
        EditorGUILayout.LabelField("Open Upgrade Color", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("nextColor"));
    }
}
