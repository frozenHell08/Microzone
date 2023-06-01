using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Healing : MonoBehaviour
{
    [SerializeField] private RealmController rController;
    [SerializeField] private CurrentCharacter ch;
    [SerializeField] private List<HealItem> healSources;
    [SerializeField] private Warning confirmMessage;
    [SerializeField] private GameObject messagePanel;
    
    private HomescreenController hsctrl;
    private GameObject buttonClicked;
    private HealItem item;
    private string splitName;

    public void ConfirmHeal() {
        if (ch.currentHealth == ch.maxHealth) {
            CancelHeal();
            return;
        }

        hsctrl = GameObject.Find("HomeScreen").GetComponent<HomescreenController>();

        FieldInfo healField = Array.Find<FieldInfo>(ch.healCount.GetType().GetFields(), 
                f => f.Name.Equals(splitName, StringComparison.OrdinalIgnoreCase));

        int itemCount = (int) healField.GetValue(ch.healCount);

        healField.SetValue(ch.healCount, itemCount - 1);

        ch.currentHealth += item.effect;

        if (ch.currentHealth > 100) ch.currentHealth = 100;

        rController.UpdateHealthInRealm();
        rController.UpdateItemInRealm(splitName, itemCount - 1);
        hsctrl.StatusInventory();
        CancelHeal();
    }

    public void ShowConfirmation() {
        buttonClicked = EventSystem.current.currentSelectedGameObject;
        splitName = buttonClicked.name.Split('_')[1];
        item = healSources.Find(i => i.name.Equals(splitName, StringComparison.OrdinalIgnoreCase));
        string message = "";

        if (ch.currentHealth == ch.maxHealth) {
            message = $"Character health is full. Items cannot be used.";
        } else {
            message = $"{confirmMessage.message} \n\nThis item {item.defi.ToLower()}";
        }

        TMP_Text txtDisplay = messagePanel.GetComponentInChildren<TMP_Text>(true);

        txtDisplay.text = message;

        messagePanel.SetActive(true);
    }

    public void CancelHeal() {
        messagePanel.SetActive(false);
    }
}

#if UNITY_EDITOR
[ CustomEditor ( typeof(Healing) ) ]
public class HealingEditor : Editor {

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Header("Source", false);
        Property("rController");
        Property("ch");

        Header("Heal Items", true);
        Property("healSources");

        Header("Confirmation", true);
        Property("confirmMessage");
        Property("messagePanel");

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