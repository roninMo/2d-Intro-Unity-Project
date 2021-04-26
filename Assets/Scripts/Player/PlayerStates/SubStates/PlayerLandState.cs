using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{

    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0);
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (input.x != 0 && isGrounded)
        {
            StateMachine.ChangeState(player.MoveState);
        }
        else if (isAnimationFinished)
        {
            StateMachine.ChangeState(player.IdleState);
        }


    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
