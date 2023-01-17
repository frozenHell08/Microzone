using UnityEngine;

[CreateAssetMenu]
public class CurrentCharacter : ScriptableObject
{
    public string characterID;
    public string characterName;
    public bool firstLogin = true;
    public Sprite sprite;
    public long cells;
    public long genes;
    public int level;
}

// first login, move to saveslot
// heals, resist, solutions, stages?