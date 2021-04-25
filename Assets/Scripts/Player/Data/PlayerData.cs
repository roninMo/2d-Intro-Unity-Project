using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/BaseData" )]

public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 25f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.4f;
    public LayerMask whatIsGround;
}
