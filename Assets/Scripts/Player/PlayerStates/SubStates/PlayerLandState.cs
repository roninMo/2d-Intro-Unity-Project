using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{

    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.setVelocityX(0);
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        
        if (input.x != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }
        else if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.IdleState);
        }

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
