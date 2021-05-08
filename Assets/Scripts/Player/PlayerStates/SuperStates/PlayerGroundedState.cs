using UnityEngine;

public class PlayerGroundedState : PlayerState
    // Right click PlayerGroundedState > Quick Actions > Generate Overrides > All but Equals, GetHashCode, and ToString
{
    protected Vector2 input; // This value is defined once here while we reference it in multiple areas within our different sub states
    protected bool jumpInput;
    private bool grabInput;
    private bool dashInput;
    private bool isTouchingGround;
    private bool isTouchingWall;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpsLeft();
        player.dashState.ResetCanDash();
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate() // This overrides the normal function, and in it we run the function plus some extra code for all things in the grounded State
    {
        base.LogicUpdate();
        input = player.InputHandler.RawMovementInput; // Now we grab the movement input in the super state to share amongst the sub states
        jumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;

        // State logic
        if (jumpInput && player.JumpState.CanJump() && Time.time >= StartTime + playerData.jumpDelay) // Jump State
        {
            StateMachine.ChangeState(player.JumpState);
        }
        else if (!isTouchingGround) // Air State
        {
            player.InAirState.StartCoyoteTime();
            StateMachine.ChangeState(player.InAirState);
        }
        else if (isTouchingWall && grabInput) // Wall Grab State
        {
            StateMachine.ChangeState(player.WallGrabState);
        }
        else if (dashInput && player.dashState.CheckIfCanDash()) // Dash State
        {
            StateMachine.ChangeState(player.dashState);
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
}
