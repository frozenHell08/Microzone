using UnityEngine;

[CreateAssetMenu]
public class CurrentCharacter : ScriptableObject
{
    public string characterID;
    public string characterName;
    public string gender;
    public bool firstLogin;
    public Sprite sprite;
    public long cells;
    public long genes;
    public int level;
    public int totalStages;
    public int res_bacteria;
    public int res_parasite;
    public int res_virus;
    public CurrentHealing healCount;
    public CurrentSolution solutionsCount;

    public void Reset() {
        characterID = "";
        characterName = "";
        firstLogin = false;
        cells = 0;
        genes = 0;
        level = 0;
        totalStages = 1;

        healCount.Reset();
        solutionsCount.Reset();
    }
}