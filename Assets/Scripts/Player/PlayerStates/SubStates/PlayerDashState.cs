using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }
    private bool dashInputStop;
    private float lastDashTime;
    private bool isHolding;
    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;
    private Vector2 lastAfterImagePosition;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        player.InputHandler.UseDashInput();
        isHolding = true;
        dashDirection = Vector2.right * player.FacingDirection;
        Time.timeScale = playerData.holdTimeScale;
        StartTime = Time.unscaledTime; // This timer won't be affected in slow motion

        player.DashDirectionIndicator.gameObject.SetActive(true);
    } 


    public override void Exit()
    {
        base.Exit();

        if (player.CurrentVelocity.y > 0)
        {
            player.SetVelocityY(player.CurrentVelocity.y * playerData.dashEndYMultiplier);
        }
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (isHolding) // While choosing a dash direction
            {
                dashDirectionInput = player.InputHandler.RawDashDirectionInput;
                dashInputStop = player.InputHandler.DashInputStop;

                if (dashDirectionInput != Vector2.zero)
                {
                    dashDirection = dashDirectionInput;
                    dashDirection.Normalize();
                }

                float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
                player.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);

                if(dashInputStop || Time.unscaledTime >= StartTime + playerData.maxHoldTime)
                {
                    isHolding = false;
                    Time.timeScale = 1f;
                    StartTime = Time.time;
                    player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    player.rb.drag = playerData.drag;
                    player.DashDirectionIndicator.gameObject.SetActive(false);
                    player.SetVelocity(playerData.dashVelocity, dashDirection);
                }
            }
            else // While dashing
            {
                player.SetVelocity(playerData.dashVelocity, dashDirection);

                if (Time.time >= StartTime + playerData.dashTime)
                {
                    player.rb.drag = 0;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
            
        }
    }

    private void PlaceAfterImage()
    {
        //PlayerAfterImagePool.Instance
    }


    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + playerData.dashCooldown;
    }


    public void ResetCanDash() => CanDash = true;
}
