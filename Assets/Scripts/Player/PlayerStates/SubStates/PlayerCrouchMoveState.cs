using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void Enter()
    {
        base.Enter();

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

            if (input.y != -1 && !willCollideWithCeiling) // Move State
            {
                StateMachine.ChangeState(player.MoveState);
            }
            else if (input.x == 0) // Crouch Idle State
            {
                StateMachine.ChangeState(player.CrouchIdleState);
            }
            else // Crouch Move State logic
            {
                Core.Movement.CheckIfShouldFlip(input.x);
                Core.Movement.SetVelocityX(playerData.crouchMovementVelocity * Core.Movement.FacingDirection);
            }
        }
    }
}
