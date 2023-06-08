using UnityEditor;

[CustomEditor(typeof(HomescreenController))]
public class HomescreenControllerEditor : Editor
{
    SerializedProperty cells;
    SerializedProperty genes;
    SerializedProperty bandaid;
    SerializedProperty milaon_s;
    bool showStatInvFields, showGiftValues = false;

    void OnEnable() {
        cells = serializedObject.FindProperty("cells");
        genes = serializedObject.FindProperty("genes");
        bandaid = serializedObject.FindProperty("bandaid");
        milaon_s = serializedObject.FindProperty("milaon_s");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("rController"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ch"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cHeal"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cSol"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("characterPanel"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("giftPanel"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("tutorialPanel"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("gendata_cells"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("gendata_genes"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("scrollbar"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("itmPanels"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("healItems"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("solnItems"));

        showGiftValues = EditorGUILayout.BeginFoldoutHeaderGroup(showGiftValues, "Gift Values");

        if (showGiftValues) {
            EditorGUILayout.PropertyField(cells);
            EditorGUILayout.PropertyField(genes);
            EditorGUILayout.PropertyField(bandaid);
            EditorGUILayout.PropertyField(milaon_s);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        showStatInvFields = EditorGUILayout.BeginFoldoutHeaderGroup(showStatInvFields, "Status Fields");

        if (showStatInvFields) {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("iName"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("iSprite"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("iStatus"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("iRes"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("iHeals"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("iSolutions"));
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        serializedObject.ApplyModifiedProperties();
    }
}
