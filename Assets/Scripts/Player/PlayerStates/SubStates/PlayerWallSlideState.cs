using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.SetVelocityY(-playerData.wallslideVelocity);
    }
}
