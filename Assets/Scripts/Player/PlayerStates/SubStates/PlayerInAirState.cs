using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private Vector2 input;
    private bool jumpInput;
    private bool isGrounded;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
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
        input = player.InputHandler.RawMovementInput;
        jumpInput = player.InputHandler.JumpInput;

        if(isGrounded)
        {
            if (jumpInput)
            {
                player.InputHandler.UseJumpInput();
                stateMachine.ChangeState(player.JumpState);
            }
            if (player.currentVelocity.y < 0.01)
            {
                stateMachine.ChangeState(player.LandState);
            }
        }
        else
        {
            player.CheckIfShouldFlip(input.x);
            player.setAirVelocityX(playerData.movementVelocity * input.x);

            player.Anim.SetFloat("yVelocity", player.currentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.currentVelocity.x));

        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfTouchingGround();
    }
}
