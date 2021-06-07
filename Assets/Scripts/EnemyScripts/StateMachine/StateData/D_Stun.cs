using UnityEngine;

[CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun State")]
public class D_Stun : ScriptableObject
{
    public float stunTime = 2f;
    public float stunKnockbackTime = 0.2f;
    public float stunKnockbackSpeed = 15f;
    public Vector2 stunKnockbackAngle;
}
