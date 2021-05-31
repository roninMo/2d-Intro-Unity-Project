using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void Enter()
    {
        base.Enter();

        Core.Movement.SetVelocityToZero();
        player.SetColliderHeight(playerData.crouchColliderHeight);
    }


    public override void Exit()
    {
        base.Exit();

        player.SetColliderHeight(playerData.standColliderHeight);
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // State logic
        if (!isExitingState)
        {
            if (input.y != -1 && !willCollideWithCeiling) // Idle State
            {
                StateMachine.ChangeState(player.IdleState);
            }
            else if (input.x != 0) // Crouch Move State
            {
                StateMachine.ChangeState(player.CrouchMoveState);
            }
        }
    }
}