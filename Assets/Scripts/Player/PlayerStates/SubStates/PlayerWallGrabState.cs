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
                StateMachine.ChangeState(player.WallClimbState);
            }
            else if (input.y < 0 || !grabInput)
            {
                StateMachine.ChangeState(player.WallSlideState);
            }
        }
    }


    private void HoldPosition() // Freeze the player, and stop the physics update velocity step
    {
        player.transform.position = holdPosition;
        Core.Movement.SetVelocityToZero();
    }
}
