using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 holdPosition;

    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void Enter()
    {
        base.Enter();

        holdPosition = player.transform.position;
        HoldPosition();
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        HoldPosition();

        // State logic
        if (input.y > 0)
        {
            StateMachine.ChangeState(player.wallClimbState);
        }
        else if (input.y < 0 || !grabInput)
        {
            StateMachine.ChangeState(player.wallSlideState);
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    public override void DoChecks()
    {
        base.DoChecks();
    }


    private void HoldPosition()
    {
        player.transform.position = holdPosition;

        player.SetVelocityX(0f);
        player.SetVelocityY(0f);
    }
}
