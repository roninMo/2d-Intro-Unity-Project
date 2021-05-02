using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState) // Do not set the velocity when exiting the state (otherwise it will cancel out other velocity calls (wallJump))
        {
            player.SetVelocityY(-playerData.wallslideVelocity);

            // State logic
            if (grabInput && input.y >= 0)
            {
                player.StateMachine.ChangeState(player.WallGrabState);
            }
        }
    }
}
