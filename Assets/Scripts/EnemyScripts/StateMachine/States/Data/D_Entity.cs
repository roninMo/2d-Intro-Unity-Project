using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float minAgroDistance = 9f;
    public float maxAgroDistance = 15f;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
