using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    //Stats
    public float maxHealth = 30f;

    //Attack mechanics
    public float closeRangeActionDistance = 2.25f;
    public float damageKnockbackForce = 10f;
    public float stunResistance = 3f;
    public float stunRecoveryTime = 1f;

    // Checks
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 1f;
    public float groundCheckRadius = 0.3f;

    public float minAgroDistance = 9f;
    public float maxAgroDistance = 15f;

    public GameObject hitParticle;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
