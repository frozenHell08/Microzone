using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ShopController : MonoBehaviour
{
    [SerializeField] private RealmController rController;
    [SerializeField] private CurrentCharacter ch;
    [SerializeField] private Warning warn;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private int solutionSRP = 5;
    [SerializeField] private TMP_Text cells;
    [SerializeField] private GameObject[] tabContent;
    private int previous, current;

    void OnEnable() {
        previous = current = 0;
        Refresh();
    }
    
    public void ConfirmPurchase() {
        GameObject clicked = EventSystem.current.currentSelectedGameObject;
        Transform parent = clicked.transform.parent;
        Transform grandparent = parent.transform.parent;
        ShopData shopData = parent.GetComponent<ShopData>();
        TMP_Text confirm = warningPanel.GetComponentInChildren<TMP_Text>(true);

        int amount = int.Parse(parent.GetComponentInChildren<TMP_InputField>().text);
        int price = 0;
        string item = "";

        if (grandparent.name.Contains("heal", StringComparison.OrdinalIgnoreCase)) {
            price = shopData.AccessHeal().price;
            item = shopData.AccessHeal().ItemName;
        } else if (grandparent.name.Contains("solution", StringComparison.OrdinalIgnoreCase)) {
            price = shopData.AccessSoln().price;
            item = shopData.AccessSoln().solutionName;
        }

        int cost = price * amount;

        if ((ch.cells - cost) < 0) {
            warn.message = $"Purchase cancelled. Not enough cells to buy {item} x{amount}.";
            confirm.text = warn.message;
            warningPanel.SetActive(true);
            Refresh();
            return;
        }

        // -------------------- SAVING CHANGES --------------------

        ch.cells -= cost;

        if (grandparent.name.Contains("heal", StringComparison.OrdinalIgnoreCase)) {
            AddToInventory(amount, shopData.AccessHeal());
            rController.SyncHealToRealm();
        } else if (grandparent.name.Contains("solution", StringComparison.OrdinalIgnoreCase)) {
            AddToInventory(amount, shopData.AccessSoln());
            rController.SyncSolnToRealm(shopData.AccessSoln().solutionName);
        }

        warn.message = $"Successfuly purchased {item} x{amount}.";
        confirm.text = warn.message;
        warningPanel.SetActive(true);

        Refresh();
    }

    private void AddToInventory(int amt, HealItem h) {
        FieldInfo field = Array.Find<FieldInfo>(ch.healCount.GetType().GetFields(),
            f => f.Name.Equals(h.ItemName, StringComparison.OrdinalIgnoreCase));

        int newval = (int) field.GetValue(ch.healCount) + (1 * amt);
        field.SetValue(ch.healCount, newval);
    }

    private void AddToInventory(int amt, Solution s) {
        FieldInfo field = Array.Find<FieldInfo>(ch.solutionsCount.GetType().GetFields(),
            f => f.Name.Equals(s.solutionName, StringComparison.OrdinalIgnoreCase));

        int newval = (int) field.GetValue(ch.solutionsCount) + (solutionSRP * amt);
        field.SetValue(ch.solutionsCount, newval);
    }
    
    private void Refresh() {
        cells.text = $"{ch.cells:n0}";

        foreach (GameObject obj in tabContent) {
            foreach (TMP_InputField inf in obj.GetComponentsInChildren<TMP_InputField>(true)) {
                inf.text = "1";
            }
        }
    }

    public void TabChange() {
        GameObject clicked = EventSystem.current.currentSelectedGameObject;
        string[] x = clicked.name.Split(" ");

        int clickIndex = Array.FindIndex(tabContent, t => t.name.StartsWith(x[0]));

        if (current != clickIndex) {
            previous = current;
            current = clickIndex;

            foreach (TMP_InputField field in tabContent[current].GetComponentsInChildren<TMP_InputField>(true)) {
                field.text = "1";
            }
        }
    }

    public void CloseMsg() {
        warningPanel.SetActive(false);
    }
    
    #if UNITY_EDITOR
    void OnValidate() {
        Refresh();
    }
    #endif
}

#if UNITY_EDITOR
[CustomEditor ( typeof (ShopController))]
public class ShopEditor : Editor {
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Property("rController");
        Property("ch", "Character");
        Property("warn", "Warning Message");
        Property("warningPanel");
        Property("solutionSRP");
        Property("cells");
        Property("tabContent");

        serializedObject.ApplyModifiedProperties();
    }

    private void Property(string name) {
        EditorGUILayout.PropertyField(serializedObject.FindProperty(name));
    }

    private void Property(string name, string label) {
        GUIContent lbl = new GUIContent(label);
        EditorGUILayout.PropertyField(serializedObject.FindProperty(name), lbl);
    }
    
    private void Header(string label) {
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
    }
}
#endif