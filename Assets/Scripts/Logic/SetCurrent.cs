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
        if (Input.GetKeyDown(KeyCode.Alpha1)) { //key 1. milaon
            Reset();
            soltxtobj = cmilaon.gameObject;
            soltxtobj.SetActive(true);
            ch.equippedSolution = solnMilaon;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) { //key 4. cinaon
            Reset();
            soltxtobj = ccinaon.gameObject;
            soltxtobj.SetActive(true);
            ch.equippedSolution = solnCinaon;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7)) { //key 7. viraon
            Reset();
            soltxtobj = cviraon.gameObject;
            soltxtobj.SetActive(true);

            ch.equippedSolution = solnViraon;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) { //key 2. mildha
            Reset();
            soltxtobj = cmildha.gameObject;
            soltxtobj.SetActive(true);

            ch.equippedSolution = solnMildha;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5)) { //key 5. cinadha
            Reset();
            soltxtobj = ccinadha.gameObject;
            soltxtobj.SetActive(true);

            ch.equippedSolution = solnCinadha;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8)) { //key 8. virdha
            Reset();
            soltxtobj = cvirdha.gameObject;
            soltxtobj.SetActive(true);

            ch.equippedSolution = solnVirdha;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) { //key 3. miltri
            Reset();
            soltxtobj = cmiltri.gameObject;
            soltxtobj.SetActive(true);

            ch.equippedSolution = solnMiltri;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6)) { //key 6. cinatri
            Reset();
            soltxtobj = ccinatri.gameObject;
            soltxtobj.SetActive(true);

            ch.equippedSolution = solnCinatri;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9)) { //key 9. virtri
            Reset();
            soltxtobj = cvirtri.gameObject;
            soltxtobj.SetActive(true);

            ch.equippedSolution = solnVirtri;
        }
    }

    private void Reset() {
        foreach (TMP_Text txt in listOfTxt) {
            GameObject txtobj = txt.gameObject;
            txtobj.SetActive(false);
        }
    }

    // private void SetDefault() {
    //     FieldInfo field = Array.Find<FieldInfo>(ch.solutionsCount.GetType().GetFields(),
    //         f => (int) f.GetValue(ch.solutionsCount) > 0);
    //         // (int) f.GetValue(soln) > 0);
    //         // f => f.Name.Equals(ch.equippedSolution.solutionName, StringComparison.OrdinalIgnoreCase));
    // }
}
