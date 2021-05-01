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

        player.SetVelocityY(playerData.wallClimbVelocity);

        if (input.y != 1)
        {
            StateMachine.ChangeState(player.wallGrabState);
        }
    }
}
