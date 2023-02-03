using UnityEngine;

[CreateAssetMenu(fileName = "Current Healing", menuName = "Current Objects/Current Healing")]
public class CurrentHealing : ScriptableObject
{
    public int Bandaid;
    public int Injector;
    public int Medicine;

    public void Reset() {
        Bandaid = 0;
        Injector = 0;
        Medicine = 0;
    }
}
