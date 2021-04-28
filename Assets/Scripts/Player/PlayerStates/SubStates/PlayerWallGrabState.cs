using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
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

        player.SetVelocityX(0f);
        player.SetVelocityY(0f);

        // State logic
        if (yInput > 0)
        {
            StateMachine.ChangeState(player.WallClimbState);
        }
        else if (yInput < 0 || !grabInput)
        {
            StateMachine.ChangeState(player.WallSlideState); 
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
}
