using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MadeEnums;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ CreateAssetMenu (fileName = "New Enemy", menuName = "Data/Enemy") ]
public class Enemy : ScriptableObject
{
    public Entry enemySource;
    public string enemyName;
    public Category category;
    public int enemyAttack;
    public int enemyHp;

    void OnValidate() {
        if (enemySource != null) {
            string[] parts = enemySource.Species.Split(' ');

            // enemyName = $"{parts[0][0]}. {parts[1]}";

            switch (category) {
                case Category.Virus :
                    if (parts.Length == 2) {
                        enemyName = enemySource.Species;
                    } else if (parts.Length == 3) {
                        enemyName = $"{parts[0][0]}. {parts[1]} {parts[2]}";    
                    }
                    
                    break;
                default :
                    enemyName = $"{parts[0][0]}. {parts[1]}";
                    break;
            }

            // if (Category.Virus) {
                
            // } else {
            //     Debug.Log("something other");
            // }
        }
    }
}

#if UNITY_EDITOR
[ CustomEditor (typeof(Enemy)) ]
public class EnemyEditor : Editor {

    public override void OnInspectorGUI() {
        serializedObject.Update();

        Header("Enemy Source", false);
        Property("enemySource");

        Header("Enemy Stats", true);
        EditorGUILayout.PropertyField(GetSeriaProperty("category"), new GUIContent("Category"));
        EditorGUILayout.Separator();
        Property("enemyName");
        Property("enemyAttack");
        Property("enemyHp");

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