using UnityEngine;

public class PlayerInAirState : PlayerState
{
    // Input
    private Vector2 input;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool grabInput;
    private bool dashInput;

    // Checks
    private bool isJumping;
    private bool isTouchingGround;
    private bool isTouchingWall;
    private bool isBackTouchingWall;
    private bool oldIsTouchingWall;
    private bool oldIsBackTouchingWall;

    // Time vars
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

        oldIsTouchingWall = false;
        oldIsBackTouchingWall = false;
        isTouchingWall = false;
        isBackTouchingWall = false;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        input = player.InputHandler.RawMovementInput;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();
        CheckJumpMultiplier(); // Controls the jump height

        // State logic
        if (player.InputHandler.AttackInputs[(int)CombatInputs.primary]) // Primary Attack State
        {
            StateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary]) // Secondary Attack State
        {
            StateMachine.ChangeState(player.SecondaryAttackState);
        }
        if (isTouchingGround && Core.Movement.CurrentVelocity.y < 0.01) // Land State
        {
            StateMachine.ChangeState(player.LandState);
        }
        else if (jumpInput && (isTouchingWall || isBackTouchingWall || wallJumpCoyoteTime)) // Wall Jump State
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = Core.CollisionSenses.WallFront;
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            StateMachine.ChangeState(player.WallJumpState);
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
        else if (isTouchingWall && input.x == Core.Movement.FacingDirection && Core.Movement.CurrentVelocity.y <= 0) // Wall Slide State 
        {
            StateMachine.ChangeState(player.WallSlideState);
        }
        else if (dashInput && player.DashState.CheckIfCanDash()) // Dash State
        {
            StateMachine.ChangeState(player.DashState);
        }
        else // While still in Air State
        {
            Core.Movement.CheckIfShouldFlip(input.x);
            Core.Movement.SetAirVelocityX(playerData.airMovementVelocity * input.x);

            player.Anim.SetFloat("yVelocity", Core.Movement.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(Core.Movement.CurrentVelocity.x));

        }
    }


    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                Core.Movement.SetVelocityY(Core.Movement.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (Core.Movement.CurrentVelocity.y <= 0)
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
        isTouchingGround = Core.CollisionSenses.Ground(player.BoxCollider);
        isTouchingWall = Core.CollisionSenses.WallFront;
        isBackTouchingWall = Core.CollisionSenses.WallBack;

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
