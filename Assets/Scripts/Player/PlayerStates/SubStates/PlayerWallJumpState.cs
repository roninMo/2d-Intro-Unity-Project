using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    private float xInput;
    private float yInput;
    private bool jumpInput;
    private bool isTouchingWall;
    private bool isBackTouchingWall;

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void Enter()
    {
        base.Enter();
        yInput = player.InputHandler.RawMovementInput.y;
        player.InputHandler.UseJumpInput();
        player.JumpState.ResetAmountOfJumpsLeft();

        // WalljumpDirection
        if (yInput > 0)
        {
            Core.Movement.SetVelocityY(playerData.verticalWallJumpVelocity);
        }
        else
        {
            Core.Movement.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
            Core.Movement.CheckIfShouldFlip(wallJumpDirection);
        }
        player.JumpState.DecreaseAmountOfJumpsLeft();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.RawMovementInput.x;
        yInput = player.InputHandler.RawMovementInput.y;
        jumpInput = player.InputHandler.JumpInput;
        player.Anim.SetFloat("yVelocity", Core.Movement.CurrentVelocity.y);
        player.Anim.SetFloat("xVelocity", Mathf.Abs(Core.Movement.CurrentVelocity.x));

        if (Time.time > StartTime + (playerData.wallJumpTime / 2.5))
        {
            Core.Movement.SetAirVelocityX(playerData.airMovementVelocity * xInput);
            Core.Movement.CheckIfShouldFlip(xInput);
        }
        if (Time.time > StartTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }

        // State Logic (If they're consecutively jumping from wall to wall)
        if (jumpInput && (isTouchingWall || isBackTouchingWall) && yInput != 1)
        {
            isTouchingWall = Core.CollisionSenses.WallFront;
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            StateMachine.ChangeState(player.WallJumpState);
        }
    }


    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -Core.Movement.FacingDirection;
        }
        else
        {
            wallJumpDirection = Core.Movement.FacingDirection;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isTouchingWall = Core.CollisionSenses.WallFront;
        isBackTouchingWall = Core.CollisionSenses.WallBack;
    }
}
