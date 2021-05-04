﻿using UnityEngine;

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
    public float wallClimbVelocity = 8f;

    [Header("Wall Jump State")]
    public float wallJumpCoyoteTime = 0.01f;
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("Dash State")]
    public float dashCooldown = 0.4f;
    public float maxHoldTime = 1f;
    public float holdTimeScale = 0.25f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30f;
    public float drag = 10f; // This affects the air density while dashing, and the majority recommend this
    public float dashEndYMultiplier = 0.5f; // This lets you hold the dash rather than pressing it and flying off in the distance
    public float distBetweenAfterImages = 0.4f;

    [Header("Ledge Climb State")]
    public Vector2 startOffset;
    public Vector2 stopOffset;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.4f;
    public LayerMask whatIsGround;
    public float wallCheckDistance = 0.6f;

    //[Header("Misc Mechanics")]
}
