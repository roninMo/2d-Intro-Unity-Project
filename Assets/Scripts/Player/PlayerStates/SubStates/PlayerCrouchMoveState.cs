using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // State logic
        if (!isExitingState)
        {

            if (input.y != -1) // Move State
            {
                StateMachine.ChangeState(player.MoveState);
            }
            else if (input.x == 0) // Crouch Idle State
            {
                StateMachine.ChangeState(player.crouchIdleState);
            }
            else // Crouch Move State logic
            {
                player.CheckIfShouldFlip(input.x);
                player.SetVelocityX(playerData.crouchMovementVelocity * player.FacingDirection);
            }
        }
    }
}
