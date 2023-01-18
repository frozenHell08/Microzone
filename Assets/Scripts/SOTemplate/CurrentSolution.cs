using UnityEngine;

[CreateAssetMenu]
public class CurrentSolution : ScriptableObject
{
    public int milaon;
    public int cinaon;
    public int viraon;
    public int mildha;
    public int cinadha;
    public int virdha;
    public int miltri;
    public int cinatri;
    public int virtri;

    public void Reset() {
        milaon = 0;
        cinaon = 0;
        viraon = 0;
        mildha = 0;
        cinadha = 0;
        virdha = 0;
        miltri = 0;
        cinatri = 0;
        virtri = 0;
    }
}
