using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void Enter()
    {
        base.Enter();

        player.SetVelocityX(0f);
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // State logic
        if (!isExitingState)
        {
            if (input.x != 0) // Move State
            {
                StateMachine.ChangeState(player.MoveState);
            }
            else if (input.y == -1) // Crouch Idle State
            {
                StateMachine.ChangeState(player.crouchIdleState);
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
    }
}
