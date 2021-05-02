using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 holdPosition;

    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void Enter()
    {
        base.Enter();

        holdPosition = player.transform.position;
        HoldPosition();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState) // Do not set the velocity when exiting the state (otherwise it will cancel out other velocity calls (wallJump))
        {
            HoldPosition();

            // State logic
            if (input.y > 0)
            {
                StateMachine.ChangeState(player.wallClimbState);
            }
            else if (input.y < 0 || !grabInput)
            {
                StateMachine.ChangeState(player.wallSlideState);
            }
        }
    }


    private void HoldPosition()
    {
        player.transform.position = holdPosition;

        player.SetVelocityX(0f);
        player.SetVelocityY(0f);
    }
}
