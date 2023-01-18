using UnityEditor;

[CustomEditor(typeof(HomescreenController))]
public class HomescreenControllerEditor : Editor
{
    SerializedProperty cells;
    SerializedProperty genes;
    SerializedProperty bandaid;
    SerializedProperty milaon_s;
    bool showGiftValues = false;

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
        EditorGUILayout.PropertyField(serializedObject.FindProperty("character"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("characterPanel"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("giftPanel"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("gendata_cells"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("gendata_genes"));

        showGiftValues = EditorGUILayout.BeginFoldoutHeaderGroup(showGiftValues, "Gift Values");

        if (showGiftValues) {
            EditorGUILayout.PropertyField(cells);
            EditorGUILayout.PropertyField(genes);
            EditorGUILayout.PropertyField(bandaid);
            EditorGUILayout.PropertyField(milaon_s);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        serializedObject.ApplyModifiedProperties();
    }
}
