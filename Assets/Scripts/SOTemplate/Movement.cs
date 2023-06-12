using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ CreateAssetMenu (fileName = "New Movement", menuName = "Character/Movement") ]
public class Movement : ScriptableObject
{
    public string gender;
    public string idle;
    public string up;
    public string down;
    public string left;
    public string right;
}

#if UNITY_EDITOR
[ CustomEditor (typeof(Movement)) ]
public class MovementEditor : Editor {

    public override void OnInspectorGUI() {
        serializedObject.Update();

        Header("Character", false);
        Property("gender");

        Header("Animation", true);
        Property("idle");
        Property("up");
        Property("down");
        Property("left");
        Property("right");

        serializedObject.ApplyModifiedProperties();
    }

    private SerializedProperty GetSeriaProperty(string name) {
        return serializedObject.FindProperty(name);
    }
    
    private void Property(string name) {
        EditorGUILayout.PropertyField(serializedObject.FindProperty(name));
    }

    private void Header(string label, bool space) {
        if (space) EditorGUILayout.Separator();
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
    }
}
#endif