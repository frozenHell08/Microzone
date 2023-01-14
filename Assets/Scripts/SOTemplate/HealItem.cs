using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class HealItem : ScriptableObject
{
    public string ItemID;
    public string ItemName;
    public Image ItemSprite;
    public string definition;
    public int price;

}
