using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Map : MonoBehaviour
{
    [SerializeField] private CurrentCharacter ch;
    [SerializeField] private GameObject mapPanel;
    [SerializeField] private string stagePrefix;
    [SerializeField] private Sprite locked, unlocked;

    void OnEnable() {
        StringComparison oic = StringComparison.OrdinalIgnoreCase;
        
        int c = 0;

        foreach (Image img in mapPanel.GetComponentsInChildren<Image>(true)) {
            if (img.name.StartsWith(stagePrefix, oic)) {
                img.preserveAspect = true;

                if (c < ch.totalStages) {
                    img.sprite = unlocked;
                    img.GetComponent<Button>().interactable = true;
                } else {
                    img.sprite = locked;
                    img.GetComponent<Button>().interactable = false;
                }
                c++;
            }
        }
    }

    public void EnterStage(string stage) {
        SceneManager.LoadScene(stage);
    }
}

#if UNITY_EDITOR
[ CustomEditor (typeof(Map)) ]
public class MapEditor : Editor {
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Header("Source");
        Property("ch");
        Property("mapPanel");
        Property("stagePrefix");
        
        Header("Sprites");
        Property("locked");
        Property("unlocked");

        serializedObject.ApplyModifiedProperties();
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