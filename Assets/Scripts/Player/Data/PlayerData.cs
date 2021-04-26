using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/BaseData" )]

public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 25f;
    public int amountOfJumps = 2;
    public float jumpDelay = 0.0125f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.4f;
    public LayerMask whatIsGround;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    //[Header("Misc Mechanics")]
}
