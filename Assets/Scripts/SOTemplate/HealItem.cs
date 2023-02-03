using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Item", menuName = "Item/Heal Item")]
public class HealItem : ScriptableObject
{
    public string ItemID;
    public string ItemName;
    public Sprite ItemSprite;
    public int effect;
    public int price;
    public string defi;

    void OnValidate() {
        defi = $"Restores health for \b{effect:n0} points.";
    }
}

#if UNITY_EDITOR
[ CustomEditor (typeof (HealItem)) ]
public class ItemEditor : Editor {
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        HealItem item = (HealItem) target;

        Header("Item Details");
        Property("ItemID");
        Property("ItemName");
        Property("ItemSprite");

        IntProperty("effect", "Heal Effect", item.effect);
        IntProperty("price", "Price", item.price);

        Header("Definition");
        EditorGUILayout.LabelField(item.defi);

        serializedObject.ApplyModifiedProperties();
    }

    private int IntProperty(string prop, string name, int value) {
        return serializedObject.FindProperty(prop).intValue = EditorGUILayout.IntField(name, value);
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
