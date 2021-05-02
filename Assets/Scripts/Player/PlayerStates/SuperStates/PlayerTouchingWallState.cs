using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool isTouchingGround;
    protected bool isTouchingWall;
    protected Vector2 input;
    protected bool grabInput;
    protected bool jumpInput;

    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
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
        grabInput = player.InputHandler.GrabInput;
        jumpInput = player.InputHandler.JumpInput;

        // State logic
        if (jumpInput)
        {
            player.wallJumpState.DetermineWallJumpDirection(isTouchingWall);
            StateMachine.ChangeState(player.wallJumpState);
        }
        else if (isTouchingGround && !grabInput) // Idle State
        {
            StateMachine.ChangeState(player.IdleState);
        }
        else if (!isTouchingWall || (input.x != player.FacingDirection && !grabInput)) // In Air State
        {
            StateMachine.ChangeState(player.InAirState);
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
