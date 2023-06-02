using UnityEngine;
using System;
using MadeEnums;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ CreateAssetMenu (fileName = "new resist", menuName = "Data/Resistances") ]
public class Resistances : ScriptableObject
{
    public Category category;
    public string catName = "";
    public string resName;
    public Sprite sprite;
    public int level = 1;
    public int resEffect = 1;
    public int price = 0;
    public string description;

    void OnValidate() {
        Func<int, int> res = x => (x * 3);
        
        switch (category) {
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

        resName = $"{catName} Resistance Lv.{level}";
        description = $"Resistance to {catName} type enemies +1.";
        price = res(level);
    }
}

#if UNITY_EDITOR
[ CustomEditor (typeof (Resistances)) ]
public class ResEditor : Editor {
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Resistances _res = (Resistances) target;
        GUIStyle style = new GUIStyle(EditorStyles.textArea);
        style.wordWrap = true;

        Header("Details");
        EditorGUILayout.PropertyField(GetSeriaProperty("category"), new GUIContent("Category"));

        Header(_res.resName);
        Property("sprite");
        IntProperty("price", "Price", _res.price);
        EditorGUILayout.Space();

        EditorGUILayout.IntSlider(GetSeriaProperty("level"), 1, 10, new GUIContent("Resistance Level"));
        EditorGUILayout.IntSlider(GetSeriaProperty("resEffect"), 1, 10);

        Header("Description");
        EditorGUILayout.LabelField(_res.description);

        serializedObject.ApplyModifiedProperties();
    }

    private int IntProperty(string prop, string name, int value) {
        return serializedObject.FindProperty(prop).intValue = EditorGUILayout.IntField(name, value);
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

