using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetCurrent : MonoBehaviour
{
    [SerializeField] private CurrentCharacter ch;

    public Solution solnMilaon;
    public Solution solnCinaon;
    public Solution solnViraon;
    public Solution solnMildha;
    public Solution solnCinadha;
    public Solution solnVirdha;
    public Solution solnMiltri;
    public Solution solnCinatri;
    public Solution solnVirtri;

    public TMP_Text cmilaon;
    public TMP_Text ccinaon;
    public TMP_Text cviraon;
    public TMP_Text cmildha;
    public TMP_Text ccinadha;
    public TMP_Text cvirdha;
    public TMP_Text cmiltri;
    public TMP_Text ccinatri;
    public TMP_Text cvirtri;

    private GameObject soltxtobj;
    private List<TMP_Text> listOfTxt = new List<TMP_Text>();

    void OnEnable() {
        listOfTxt.Add(cmilaon);
        listOfTxt.Add(ccinaon);
        listOfTxt.Add(cviraon);
        listOfTxt.Add(cmildha);
        listOfTxt.Add(ccinadha);
        listOfTxt.Add(cvirdha);
        listOfTxt.Add(cmiltri);
        listOfTxt.Add(ccinatri);
        listOfTxt.Add(cvirtri);
    }

    void Update()
    {
        Dictionary<KeyCode, (Solution, GameObject)> keyMappings = new Dictionary<KeyCode, (Solution, GameObject)>() {
            { KeyCode.Alpha1, (solnMilaon, cmilaon.gameObject) },
            { KeyCode.Alpha2, (solnMildha, cmildha.gameObject) },
            { KeyCode.Alpha3, (solnMiltri, cmiltri.gameObject) },
            { KeyCode.Alpha4, (solnCinaon, ccinaon.gameObject) },
            { KeyCode.Alpha5, (solnCinadha, ccinadha.gameObject) },
            { KeyCode.Alpha6, (solnCinatri, ccinatri.gameObject) },
            { KeyCode.Alpha7, (solnViraon, cviraon.gameObject) },
            { KeyCode.Alpha8, (solnVirdha, cvirdha.gameObject) },
            { KeyCode.Alpha9, (solnVirtri, cvirtri.gameObject) },
        };

        foreach (var kvp in keyMappings) {
            if (Input.GetKeyDown(kvp.Key)) {
                FieldInfo solField = ch.solutionsCount.GetType().GetFields().FirstOrDefault(f => f.Name.Equals(kvp.Value.Item1.solutionName, StringComparison.OrdinalIgnoreCase));

                int itemCount = (int) solField.GetValue(ch.solutionsCount);

                if (itemCount == 0) return;

                Reset();
                soltxtobj = kvp.Value.Item2;
                soltxtobj.SetActive(true);

                ch.equippedSolution = kvp.Value.Item1;
                break;
            }
        }
    }

    private void Reset() {
        foreach (TMP_Text txt in listOfTxt) {
            GameObject txtobj = txt.gameObject;
            txtobj.SetActive(false);
        }
    }
}