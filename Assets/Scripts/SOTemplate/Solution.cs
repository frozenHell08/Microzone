using UnityEngine;
using System;
using MadeEnums;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Solution", menuName = "Item/Solution")]
public class Solution : ScriptableObject
{
    public Category _category;
    public string solutionID;
    public string solutionName;
    public Sprite solutionSprite;
    public int price;
    public int attackPoints;
    public string description;
    
    void OnValidate() {
        string catName = "";

        switch (_category) {
            case Category.Bacteria :
                catName = Enum.GetName(typeof(Category), Category.Bacteria);
                break;
            case Category.Parasite : 
                catName = Enum.GetName(typeof(Category), Category.Parasite);
                break;
            case Category.Virus :
                catName = Enum.GetName(typeof(Category), Category.Virus);
                break;
        }
        description = $"Damage against {catName} +{attackPoints}.";
    }
}

#if UNITY_EDITOR
[ CustomEditor (typeof(Solution)) ]
public class SolnEditor : Editor {
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        Solution solution = (Solution) target;

        EditorGUILayout.PropertyField(GetSeriaProperty("_category"), new GUIContent("Category"));

        Header("Solution Details");
        Property("solutionID");
        Property("solutionName");
        Property("solutionSprite");
        IntProperty("price", "Price", solution.price);

        Header("Properties");
        IntProperty("attackPoints", "Attack Power", solution.attackPoints);

        Header("Description");
        EditorGUILayout.LabelField(solution.description);

        serializedObject.ApplyModifiedProperties();
    }

    private int IntProperty(string prop, string name, int value) {
        return serializedObject.FindProperty(prop).intValue = EditorGUILayout.IntField(name, value);
    }

    private void Property(string name) {
        EditorGUILayout.PropertyField(serializedObject.FindProperty(name));
    }

    private SerializedProperty GetSeriaProperty(string name) {
        return serializedObject.FindProperty(name);
    }

    private void Header(string label) {
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
    }
}
#endif