using UnityEngine;

[CreateAssetMenu(fileName = "BlockingAndParrying", menuName = "Scriptable Objects/BlockingAndParrying")]
public class BlockingAndParrying : ScriptableObject
{
    public bool hitPlayer = false;
    public bool isBlocking;
    public bool playerParry;

    void OnEnable()
    {
        hitPlayer = false;
        isBlocking = false;
        playerParry = false;
    }
}