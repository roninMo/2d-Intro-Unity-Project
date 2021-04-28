using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.SetVelocityY(playerData.wallClimbVelocity);

        // State logic
        if (yInput != 1)
        {
            StateMachine.ChangeState(player.WallGrabState);
        }
    }
}
