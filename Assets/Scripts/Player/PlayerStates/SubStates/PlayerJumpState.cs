using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }


    public override void Enter()
    {
        base.Enter();


        player.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        amountOfJumpsLeft--;
        Debug.Log("Jumps left: " + amountOfJumpsLeft);
        player.InAirState.SetIsJumping();
    }


    public bool CanJump()
    {
        if (amountOfJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void ResetAmountOfJumpsLeft()
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }
    public void DecreaseAmountOfJumpsLeft()
    {
        amountOfJumpsLeft--;
        Debug.Log("Jumps left call: " + amountOfJumpsLeft);
    }
}
