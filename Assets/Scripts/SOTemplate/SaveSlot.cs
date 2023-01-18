using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class SaveSlot : ScriptableObject
{
    public int slotNumber;
    public string ID;
    public string Name = "No Data";
    public string Gender = "No Data";
    public Sprite Sprite; 
    public long Cells;
    public long Genes;
    public int Level;
    public bool forDeletion = false;
    public bool firstLogin = false;
}
