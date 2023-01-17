using UnityEditor;

[CustomEditor(typeof(ScreenController))]
public class ScreenCtrlEditor : Editor
{
    SerializedProperty welcome;
    SerializedProperty home;
    SerializedProperty create;
    SerializedProperty load;
    SerializedProperty sinv;
    SerializedProperty compendium;
    SerializedProperty minigame;
    SerializedProperty immune;
    SerializedProperty shop;
    SerializedProperty settings;
    SerializedProperty btnStart;
    SerializedProperty btnLoad;

    bool showScreens, showGameInterface, showButtons = false;

    void OnEnable() {
        welcome = serializedObject.FindProperty("welcomeScreen");
        home = serializedObject.FindProperty("homeScreen");
        create = serializedObject.FindProperty("characterCreate");
        load = serializedObject.FindProperty("loadCharacter");
        sinv = serializedObject.FindProperty("statusInventory");
        compendium = serializedObject.FindProperty("compendium");
        minigame = serializedObject.FindProperty("minigame");
        immune = serializedObject.FindProperty("immuneSystem");
        shop = serializedObject.FindProperty("shop");
        settings = serializedObject.FindProperty("settings");
        btnStart = serializedObject.FindProperty("newGame");
        btnLoad = serializedObject.FindProperty("loadGame");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("rController"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("isReturning"));

        showScreens = EditorGUILayout.BeginFoldoutHeaderGroup(showScreens, "Intro Screens");
        
        if (showScreens) {
            EditorGUILayout.PropertyField(welcome);
            EditorGUILayout.PropertyField(create);
            EditorGUILayout.PropertyField(load);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        showGameInterface = EditorGUILayout.BeginFoldoutHeaderGroup(showGameInterface, "In-game Screens");

        if (showGameInterface) {
            EditorGUILayout.PropertyField(home);
            EditorGUILayout.PropertyField(sinv);
            EditorGUILayout.PropertyField(compendium);
            EditorGUILayout.PropertyField(minigame);
            EditorGUILayout.PropertyField(immune);
            EditorGUILayout.PropertyField(shop);
            EditorGUILayout.PropertyField(settings);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        showButtons = EditorGUILayout.BeginFoldoutHeaderGroup(showButtons, "Title Buttons");

        if (showButtons) {
            EditorGUILayout.PropertyField(btnStart);
            EditorGUILayout.PropertyField(btnLoad);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
