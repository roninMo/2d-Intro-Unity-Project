using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private Vector2 input;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool grabInput;
    private bool isTouchingGround;
    private bool isTouchingWall;
    private bool isBackTouchingWall;
    private bool oldIsTouchingWall;
    private bool oldIsBackTouchingWall;
    private bool isJumping;
    private bool coyoteTime;
    private bool wallJumpCoyoteTime;
    private float wallJumpCoyoteTimeStart;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void Enter()
    {
        base.Enter();
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        input = player.InputHandler.RawMovementInput;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        grabInput = player.InputHandler.GrabInput;

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();
        CheckJumpMultiplier(); // Controls the jump height

        // State logic
        if (isTouchingGround && player.CurrentVelocity.y < 0.01) // Land State
        {
            StateMachine.ChangeState(player.LandState);
        }
        else if (jumpInput && (isTouchingWall || isBackTouchingWall || wallJumpCoyoteTime)) // Wall Jump State
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = player.CheckIfTouchingWall();
            player.wallJumpState.DetermineWallJumpDirection(isTouchingWall);
            StateMachine.ChangeState(player.wallJumpState);
        }
        else if (jumpInput && player.JumpState.CanJump()) // Jump State
        {
            StopCoyoteTime();
            StateMachine.ChangeState(player.JumpState);
        }
        else if (isTouchingWall && grabInput)
        {
            StateMachine.ChangeState(player.wallGrabState);
        }
        else if (isTouchingWall && input.x == player.FacingDirection && player.CurrentVelocity.y <= 0) // Wall Slide State 
        {
            StateMachine.ChangeState(player.wallSlideState);
        }
        else // While still in Air State
        {
            player.CheckIfShouldFlip(input.x);
            player.SetAirVelocityX(playerData.airMovementVelocity * input.x);

            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));

        }
    }


    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0)
            {
                isJumping = false;
            }
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    public override void DoChecks()
    {
        base.DoChecks();
        oldIsTouchingWall = isTouchingWall;
        oldIsBackTouchingWall = isBackTouchingWall;
        isTouchingGround = player.CheckIfTouchingGround();
        isTouchingWall = player.CheckIfTouchingWall();
        isBackTouchingWall = player.CheckIfBackTouchingWall();

        if (!wallJumpCoyoteTime && !isTouchingWall && !isBackTouchingWall && (oldIsTouchingWall || oldIsBackTouchingWall))
        {
            StartWallJumpCoyoteTime();
        }
    }


    #region Other Functions
    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time >= StartTime + playerData.coyoteTime)
        {
            Debug.Log("Coyote limit reached");
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;
    public void StopCoyoteTime() => coyoteTime = false;

    private void CheckWallJumpCoyoteTime()
    {
        if(wallJumpCoyoteTime && Time.time > wallJumpCoyoteTimeStart + playerData.wallJumpCoyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }

    private void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        wallJumpCoyoteTimeStart = Time.time;
    }

    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;
    public void SetIsJumping() => isJumping = true;
    #endregion
}
