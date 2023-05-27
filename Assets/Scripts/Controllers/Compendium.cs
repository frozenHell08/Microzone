using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Compendium : MonoBehaviour
{

    [SerializeField] private CurrentCharacter ch;
    [SerializeField] private GameObject contentPanel;
    [SerializeField] private List<Entry> infoEntry;

    private Transform childrenlist; 

    void OnEnable() {
        Debug.Log("Open Compendium");

        childrenlist = contentPanel.transform;

        SetData(childrenlist);

        for (int l = 1; l <= ch.totalStages; l++) {
            Transform child = childrenlist.GetChild(l-1);
            
            GameObject childobject = child.gameObject;
            childobject.SetActive(true);
        }

        RectTransform rect = contentPanel.GetComponent<RectTransform>();
        AdjustHeight(rect);
    }

    public void SetData(Transform transformList) {
        for (int i = 0; i < infoEntry.Count; i++) {
            Transform child = transformList.GetChild(i);

            TMP_Text infotext = child.GetComponentInChildren<TMP_Text>(true);

            Image infoimage = Array.Find<Image>(child.GetComponentsInChildren<Image>(), 
                img => img.name == "Image");

            infoimage.preserveAspect = true;
            infoimage.sprite = infoEntry[i].entryImage;

            string symptomslist = string.Join(",\n", infoEntry[i].Symptoms);

            List<string> trans = new List<string>();

            if (infoEntry[i].directContact) trans.Add("Direct contact");
            if (infoEntry[i].dropletSpread) trans.Add("Droplet spread");
            if (infoEntry[i].airborneTransmission) trans.Add("Airborne Transmission");
            if (infoEntry[i].vehicles) trans.Add("Vehicles");

            string translist = string.Join(", ", trans);

            string info =   $"<u><b>{infoEntry[i].Species}</b></u>\n\n" +
                            $"Taxonomy\n" +
                            $"{infoEntry[i].Kingdom} > {infoEntry[i].Phylum} > {infoEntry[i].Class} > {infoEntry[i].Order}" +
                            $"{infoEntry[i].Family} > {infoEntry[i].Genus} > {infoEntry[i].Species}\n\n" +
                            $"<b>Characteristics</b>\n" +
                            $"{infoEntry[i].Characteristics}\n\n" +
                            $"<b>Modes of Transmission</b>\n" +
                            $"{translist}\n\n" +
                            $"<b>Symptoms</b>\n" +
                            $"{symptomslist}\n\n" +
                            $"<u><b>Trivias</b></u>\n" +
                            $"{infoEntry[i].trivia_1}\n\n" +
                            $"{infoEntry[i].trivia_2}\n\n" +
                            $"{infoEntry[i].trivia_3}\n\n";

            infotext.text = info;
        }
    }

    public void AdjustHeight(RectTransform content) {
        int visibleChildren = CountVisibleChildren();

        float childHeight = 0f;
        if (visibleChildren > 0) {
            RectTransform firstChild = content.GetChild(0).GetComponent<RectTransform>();
            childHeight = firstChild.rect.height;
        }

        float panelHeight = visibleChildren * childHeight;
        Vector2 sizeDelta = content.sizeDelta;
        sizeDelta.y = panelHeight + 350;
        content.sizeDelta = sizeDelta;
    }

    private int CountVisibleChildren() {
        int count = 0;

        for (int i = 0; i < childrenlist.childCount; i++) {
            Transform child = childrenlist.GetChild(i);
            if (child.gameObject.activeSelf)
            {
                count++;
            }
        }
        return count;
    }

}

#if UNITY_EDITOR
[ CustomEditor ( typeof(Compendium) ) ]
public class CompendiumEditor : Editor {

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Header("Source");
        Property("ch");
        Property("contentPanel");
        Property("infoEntry");

        serializedObject.ApplyModifiedProperties();
    }

    private SerializedProperty GetSeriaProperty(string name) {
        return serializedObject.FindProperty(name);
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