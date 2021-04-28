using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private Vector2 input;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool grabInput;
    private bool isTouchingGround;
    private bool isTouchingWall;
    private bool isJumping;
    private bool coyoteTime;

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
        CheckJumpMultiplier(); // Controls the jump height

        // State logic
        if (isTouchingGround && player.CurrentVelocity.y < 0.01) // Land State
        {
            StateMachine.ChangeState(player.LandState);
        }
        else if (jumpInput && player.JumpState.CanJump()) // Jump State
        {
            StopCoyoteTime();
            StateMachine.ChangeState(player.JumpState);
        }
        else if (isTouchingWall && grabInput) // Wall Grab State
        {
            StateMachine.ChangeState(player.WallGrabState);
        }
        else if (isTouchingWall && input.x == player.FacingDirection && player.CurrentVelocity.y < 0.01) // Wall Slide State
        {
            StateMachine.ChangeState(player.WallSlideState);
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
        isTouchingGround = player.CheckIfTouchingGround();
        isTouchingWall = player.CheckIfTouchingWall();
    }


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
    public void SetIsJumping() => isJumping = true;
}
