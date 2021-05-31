using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void Enter()
    {
        base.Enter();
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Core.Movement.CheckIfShouldFlip(input.x);
        Core.Movement.SetVelocityX(playerData.movementVelocity * input.x);

        // State logic
        if (!isExitingState)
        {
            if (input.x == 0) // Idle State
            {
                StateMachine.ChangeState(player.IdleState);
            }
            else if (input.y == -1) // Crouch Move State
            {
                StateMachine.ChangeState(player.CrouchMoveState);
            }
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    public override void DoChecks()
    {
        base.DoChecks();
    }
}
