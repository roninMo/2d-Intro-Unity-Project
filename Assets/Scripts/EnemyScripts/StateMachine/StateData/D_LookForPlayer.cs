using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerDetectedStateData", menuName = "Data/State Data/LookForPlayer State")]
public class D_LookForPlayer : ScriptableObject
{
    public int amountOfTurns = 2;
    public float minTimeBetweenTurns = 0.64f;
    public float maxTimeBetweenTurns = 1.28f;
}
