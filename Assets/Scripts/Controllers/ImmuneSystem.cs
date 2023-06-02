using System;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ImmuneSystem : MonoBehaviour
{
    [SerializeField] private RealmController rController;
    [SerializeField] private CurrentCharacter ch;
    [SerializeField] private Warning warn;
    [SerializeField] private Static _stat;
    [SerializeField] private TMP_Text genes;
    [SerializeField] private TMP_Text summary;
    [SerializeField] private GameObject panelDisplay;
    [SerializeField] private GameObject panelBact;
    [SerializeField] private GameObject panelPara;
    [SerializeField] private GameObject panelVir;
    [SerializeField] private GameObject panelWarn;
    [SerializeField] private Color upgradeOpen;
    [SerializeField] private Color disabled;

    void OnEnable() {
        ResetBoard(panelBact, panelPara, panelVir);
        Refresh();
    }

    private void Refresh() {
        Func<int, int> res = x => (x * 3);
        genes.text = $"{ch.genes:n0}";

        // -------------------- IS LEVELS --------------------
        
        ButtonLoad(panelBact, ch.res_bacteria);
        ButtonLoad(panelPara, ch.res_parasite);
        ButtonLoad(panelVir, ch.res_virus);

        // -------------------- SUMMARY --------------------

        string resFor = $"<b>Bacteria</b> Lv.{ch.res_bacteria}: \t{res(ch.res_bacteria)}\n" +
                        $"<b>Parasite</b> Lv.{ch.res_parasite}:\t{res(ch.res_parasite)}\n" +
                        $"<b>Virus</b> Lv.{ch.res_virus}:\t\t{res(ch.res_virus)}";

        summary.text = resFor;

        // -------------------- CLEAR DISPLAY --------------------
        
        Image displaySprite = panelDisplay.GetComponentInChildren<Image>();

        displaySprite.enabled = false;

        foreach (TMP_Text txt in panelDisplay.GetComponentsInChildren<TMP_Text>()) {
            txt.text = "";
        }
    }

    private void ButtonLoad(GameObject panel, int level) {
        ColorBlock target = ColorBlock.defaultColorBlock;

        try {
            for (int i = 0; i <= level; i++) {
                Button btn = panel.transform.GetChild(i).gameObject.GetComponent<Button>();
                
                if (i < level) {
                        target.disabledColor = new Color(1, 1, 1, 1);
                        btn.colors = target;
                        btn.interactable = false;
                } else if (i == level) {
                        target.normalColor = upgradeOpen;
                        btn.colors = target;
                        btn.interactable = true;
                }
            }
        } catch (UnityException) {}
    }

    public void ConfirmUpgrade() {
        TMP_Text txtprice = Array.Find<TMP_Text>(panelDisplay.GetComponentsInChildren<TMP_Text>(),
            t => t.name.Equals("price", StringComparison.OrdinalIgnoreCase));

        int _price = int.Parse(txtprice.text);

        if ((ch.genes - _price) < 0) {
            warn.message = "You do not have enough genes.";
            MsgtoPanel();
            return;
        }

        ch.genes -= _price;

        FieldInfo field = Array.Find<FieldInfo>(ch.GetType().GetFields(),
                        f => f.Name.EndsWith(_stat.stringValue, StringComparison.OrdinalIgnoreCase));

        field.SetValue(ch, (int) field.GetValue(ch) + 1);

        rController.SyncResToRealm();
        Refresh();

        warn.message = $"Successfully upgraded {_stat.stringValue} to level {_stat.doubleValue:n0}.";
        MsgtoPanel();
    }

    private void MsgtoPanel() {
        TMP_Text msg = panelWarn.GetComponentInChildren<TMP_Text>(true);
        msg.text = warn.message;
        panelWarn.SetActive(true);
    }
    
    public void BackToIS() {
        panelWarn.SetActive(false);
        ResetStatic();
    }

    private void ResetStatic() {
        _stat.stringValue = "";
        _stat.intValue = 0;
        _stat.doubleValue = 0;
    }
    
    public void DisplayDetails(Resistances res) {
        StringComparison oic = StringComparison.OrdinalIgnoreCase;
        Image displaySprite = panelDisplay.GetComponentInChildren<Image>();

        displaySprite.enabled = true;
        displaySprite.preserveAspect = true;
        displaySprite.sprite = res.sprite;

        foreach (TMP_Text txt in panelDisplay.GetComponentsInChildren<TMP_Text>()) {
            if (txt.name.Contains("Name", oic)) txt.text = res.resName;
            if (txt.name.Contains("description", oic)) txt.text = res.description;
            if (txt.name.Contains("price", oic)) txt.text = $"{res.price:n0}";
        }

        _stat.stringValue = res.catName;
        _stat.intValue = res.price;
        _stat.doubleValue = res.level;
    }

    private void ResetBoard(GameObject _panel1, GameObject _panel2, GameObject _panel3) {
        ColorBlock target = ColorBlock.defaultColorBlock;
        Button btn, btn2, btn3;

        for (int i = 0; i < 10; i++ ) {
            btn = _panel1.transform.GetChild(i).gameObject.GetComponent<Button>();
            btn2 = _panel2.transform.GetChild(i).gameObject.GetComponent<Button>();
            btn3 = _panel3.transform.GetChild(i).gameObject.GetComponent<Button>();

            target.normalColor = disabled;
            btn.colors = target;
            btn2.colors = target;
            btn3.colors = target;

            btn.interactable = false;
            btn2.interactable = false;
            btn3.interactable = false;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor ( typeof (ImmuneSystem))]
public class ISEditor : Editor {

    bool showISFields = false;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Header("References");
        Property("rController");
        Property("ch");
        Property("warn");
        Property("_stat");

        Header("Objects");
        Property("summary");
        Property("panelDisplay");
        Property("panelWarn");

        Header("Currencies");
        Property("genes");
        
        Header("Colors");
        Property("upgradeOpen");
        Property("disabled");

        EditorGUILayout.Space();
        showISFields = EditorGUILayout.BeginFoldoutHeaderGroup(showISFields, "IS Panels");

        if (showISFields) {
            Property("panelBact");
            Property("panelPara");
            Property("panelVir");
        }
        
        EditorGUILayout.EndFoldoutHeaderGroup();
        serializedObject.ApplyModifiedProperties();
    }

    private void Header(string label) {
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
    }

    private void Property(string name) {
        EditorGUILayout.PropertyField(serializedObject.FindProperty(name));
    }
}
#endif