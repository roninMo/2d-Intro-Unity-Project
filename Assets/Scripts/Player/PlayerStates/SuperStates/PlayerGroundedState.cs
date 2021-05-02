using UnityEngine;

public class PlayerGroundedState : PlayerState
    // Right click PlayerGroundedState > Quick Actions > Generate Overrides > All but Equals, GetHashCode, and ToString
{
    protected Vector2 input; // This value is defined once here while we reference it in multiple areas within our different sub states
    protected bool jumpInput;
    private bool grabInput;
    private bool isTouchingGround;
    private bool isTouchingWall;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpsLeft();
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate() // This overrides the normal function, and in it we run the function plus some extra code for all things in the grounded State
    {
        base.LogicUpdate();
        input.x = player.InputHandler.RawMovementInput.x; // Now we grab the movement input in the super state to share amongst the sub states
        jumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;

        // State logic
        if (jumpInput && player.JumpState.CanJump() && Time.time >= StartTime + playerData.jumpDelay) // The delay fixes a bug
        {
            StateMachine.ChangeState(player.JumpState);
        }
        else if (!isTouchingGround)
        {
            player.InAirState.StartCoyoteTime();
            StateMachine.ChangeState(player.InAirState);
        }
        else if (isTouchingWall && grabInput)
        {
            StateMachine.ChangeState(player.wallGrabState);
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
