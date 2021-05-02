using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void Enter()
    {
        base.Enter();

        player.SetVelocityY(playerData.wallClimbVelocity);
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState) // Do not set the velocity when exiting the state (otherwise it will cancel out other velocity calls (wallJump))
        {
            player.SetVelocityY(playerData.wallClimbVelocity);

            // State logic
            if (input.y != 1)
            {
                StateMachine.ChangeState(player.WallGrabState);
            }
        }
    }
}
