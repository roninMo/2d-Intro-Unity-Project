using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    private bool finishAnimation;
    private bool isTouchingGround;

    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void Enter()
    {
        base.Enter();

        if (input.x == 0)
        {
            finishAnimation = true;
            Core.Movement.SetVelocityX(0);
        }
        else
        {
            finishAnimation = false;
        }
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // State logic
        if (!isExitingState)
        {
            if (finishAnimation)
            {
                if (input.x != 0) // If they decide to move during the animation
                {
                    StateMachine.ChangeState(player.MoveState);
                }
                else if (isAnimationFinished) // If they stand still, finish the animation
                {
                    StateMachine.ChangeState(player.IdleState);
                }
            }
            else if (Core.Movement.CurrentVelocity.x != 0 && isTouchingGround) // If they landing moving, don't freeze them in a landing animation
            {
                StateMachine.ChangeState(player.MoveState);
            }
            else // Else if they land without moving, go to Idle State
            {
                StateMachine.ChangeState(player.IdleState);
            }
        }
    }


    public override void DoChecks()
    {
        base.DoChecks();
        isTouchingGround = Core.CollisionSenses.Ground(player.BoxCollider);
    }
}
