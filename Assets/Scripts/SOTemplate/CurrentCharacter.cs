using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Current Objects/Current Character")]
public class CurrentCharacter : ScriptableObject
{
    public string characterID, characterName;
    // public string ;
    public string gender;
    public bool firstLogin;
    public Sprite sprite;
    public long cells, genes;
    // public long ;
    public int level, experience, currentHealth, maxHealth;
    public int res_bacteria;
    public int res_parasite;
    public int res_virus;
    public int totalStages;
    public CurrentHealing healCount;
    public CurrentSolution solutionsCount;

    public void Reset() {
        characterID = "";
        characterName = "";
        gender = "";
        firstLogin = false;
        cells = 0;
        genes = 0;
        level = 0;
        experience = 0;
        totalStages = 1;
        res_bacteria = 0;
        res_parasite = 0;
        res_virus = 0;

        healCount.Reset();
        solutionsCount.Reset();
    }
}