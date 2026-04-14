using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthValues", menuName = "Scriptable Objects/HealthValues", order = 1)]
public class HealthValues : ScriptableObject
{
    public int maxPlayerHealth = 100;
    public int maxEnemyHealth = 100;
}
