using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void Enter()
    {
        base.Enter();

        player.SetVelocityToZero();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // State logic
        if (!isExitingState)
        {
            if (input.y != -1) // Idle State
            {
                StateMachine.ChangeState(player.IdleState);
            }
            else if (input.x != 0) // Crouch Move State
            {
                StateMachine.ChangeState(player.crouchMoveState);
            }
        }
    }
}
