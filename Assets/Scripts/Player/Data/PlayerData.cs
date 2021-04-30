using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/BaseData" )]

public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;
    public float airMovementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 25f;
    public int amountOfJumps = 2;
    public float jumpDelay = 0.0125f;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float wallslideVelocity = 4f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 4f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.4f;
    public LayerMask whatIsGround;
    public float wallCheckDistance = 0.6f;

    //[Header("Misc Mechanics")]
}
