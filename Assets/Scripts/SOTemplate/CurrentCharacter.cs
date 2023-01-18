using UnityEngine;

[CreateAssetMenu]
public class CurrentCharacter : ScriptableObject
{
    public string characterID;
    public string characterName;
    public bool firstLogin;
    public Sprite sprite;
    public long cells;
    public long genes;
    public int level;

    public void Reset() {
        characterID = "";
        characterName = "";
        firstLogin = false;
        cells = 0;
        genes = 0;
        level = 0;
    }
}

// first login, move to saveslot
// heals, resist, solutions, stages?