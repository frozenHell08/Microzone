using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ CreateAssetMenu (fileName = "New Entry", menuName = "Data/Entry") ]
public class Entry : ScriptableObject
{
    public string Kingdom, Phylum, Class, Order, Family, Genus, Species;
    [TextArea(3, 8)]
    public string Characteristics;
    // public ModesOfTrans modes;
    [TextArea(3, 5)]
    public List<string> Symptoms;
    [TextArea(3, 8)]
    public string trivia_1, trivia_2, trivia_3;

    public bool directContact;
    public bool dropletSpread;
    public bool airborneTransmission;
    public bool vehicles;
    public bool vectors;
    public List<string> listOfVectors;
}

#if UNITY_EDITOR
[ CustomEditor (typeof(Entry)) ]
public class EntryEditor : Editor {

    bool showTrivias = false, showTaxonomy = true, showInformation = true;

    public override void OnInspectorGUI() {
        serializedObject.Update();

        Entry entry = (Entry) target;

        showTaxonomy = EditorGUILayout.BeginFoldoutHeaderGroup(showTaxonomy, "Taxonomy");

        if (showTaxonomy) {
            Property("Kingdom"); Property("Phylum"); Property("Class"); 
            Property("Order"); Property("Family"); Property("Genus");
            Property("Species");    
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        GUIStyle style = EditorStyles.foldout;
        FontStyle prev = style.fontStyle;
        style.fontStyle = FontStyle.Bold;


        EditorGUILayout.Space();
        showInformation = EditorGUILayout.Foldout(showInformation, "Information", true, style);
        style.fontStyle = prev;

        if (showInformation) {
            HeaderField("Characteristics", "Characteristics", false);
            Header("Modes of Transmission", true);

            EditorGUI.indentLevel++;
            Property("directContact"); Property("dropletSpread"); Property("airborneTransmission");
            Property("vehicles"); Property("vectors");

            if (GetSeriaProperty("vectors").boolValue) {
                EditorGUILayout.Space();
                Property("listOfVectors");
            }

            EditorGUILayout.Space();
            Property("Symptoms");
            EditorGUI.indentLevel--;
        }
        
        showTrivias = EditorGUILayout.BeginFoldoutHeaderGroup(showTrivias, "Trivias");

        if (showTrivias) {
            HeaderField("Trivia #1", "trivia_1", false);
            HeaderField("Trivia #2", "trivia_2", true);
            HeaderField("Trivia #3", "trivia_3", true);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        serializedObject.ApplyModifiedProperties();
    }

    private void HeaderField(string header, string property, bool space) {
        EditorStyles.label.richText = true;
        GUIContent con = new GUIContent();
        con.text = $"<b>{header}</b>";

        if (space) EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty(property), con);
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