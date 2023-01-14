using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class SaveSlot : ScriptableObject
{
    public int slotNumber;
    public string Name;
    public string Gender;
    public Image Sprite; 
    public long Cells;
    public long Genes;
    public int Level;
}
