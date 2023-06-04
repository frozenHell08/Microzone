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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { //key 1. milaon


            ch.equippedSolution = solnMilaon;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) { //key 2. milaon


            ch.equippedSolution = solnCinaon;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) { //key 3. viraon


            ch.equippedSolution = solnViraon;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) { //key 4. mildha


            ch.equippedSolution = solnMildha;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5)) { //key 5. cinadha


            ch.equippedSolution = solnCinadha;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6)) { //key 6. virdha


            ch.equippedSolution = solnVirdha;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7)) { //key 7. miltri


            ch.equippedSolution = solnMiltri;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8)) { //key 8. cinatri


            ch.equippedSolution = solnCinatri;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9)) { //key 9. virtri


            ch.equippedSolution = solnVirtri;
        }
    }


}
