using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    private float xInput;

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        player.JumpState.ResetAmountOfJumpsLeft();
        player.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        player.CheckIfShouldFlip(wallJumpDirection);
        player.JumpState.DecreaseAmountOfJumpsLeft();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.RawMovementInput.x;
        player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
        player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));

        if (Time.time > StartTime + (playerData.wallJumpTime / 2.5))
        {
            player.SetAirVelocityX(playerData.airMovementVelocity * xInput);
            player.CheckIfShouldFlip(xInput);
        }
        if (Time.time > StartTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }


    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -player.FacingDirection;
        }
        else
        {
            wallJumpDirection = player.FacingDirection;
        }
    }
}
