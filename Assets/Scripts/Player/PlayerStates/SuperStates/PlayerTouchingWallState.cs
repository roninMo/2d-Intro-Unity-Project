using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool isTouchingGround;
    protected bool isTouchingWall;
    protected bool grabInput;
    protected float xInput;
    protected float yInput;

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

        xInput = player.InputHandler.RawMovementInput.x;
        yInput = player.InputHandler.RawMovementInput.y;
        grabInput = player.InputHandler.GrabInput;

        if (isTouchingGround)
        {
            StateMachine.ChangeState(player.IdleState);
        }
        else if (!isTouchingWall || xInput != player.FacingDirection)
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
