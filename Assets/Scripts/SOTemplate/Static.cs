using UnityEngine;

[CreateAssetMenu(fileName = "newStatic", menuName = "Data/Static")]
public class Static : ScriptableObject
{
    public string stringValue;
    public int intValue;
    public bool boolValue;
    public float floatValue;
    public double doubleValue;
}
