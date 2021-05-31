using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D Rb { get; private set; }
    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; } // Instead of calling rb.velocity.x/y multiple times, save memory and call it once with Current Velocity

    private Vector2 workspace;


    protected override void Awake()
    {
        base.Awake();

        Rb = GetComponentInParent<Rigidbody2D>();
        FacingDirection = 1;
    }

    public void LogicUpdate()
    {
        CurrentVelocity = Rb.velocity;
    }


    #region Set Functions
    public void SetVelocityToZero()
    {
        Rb.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }


    public void SetVelocityX(float velocity)
    {
        workspace.Set((Time.fixedDeltaTime * 54) * velocity, CurrentVelocity.y);
        Rb.velocity = workspace;
        CurrentVelocity = workspace; // Since we're changing the velocity to avoid the physics/logic update overwriting each other, set the current velocity to the new velocity
    }


    public void SetAirVelocityX(float velocity)
    {
        workspace.Set((Time.fixedDeltaTime * 54) * velocity, 0);
        Rb.AddForce(workspace, ForceMode2D.Force);
        workspace.Set(Rb.velocity.x, Rb.velocity.y); // Use the current velocity value instead maybe?
        workspace.x = Mathf.Clamp(workspace.x, -9.4f, 9.4f);
        Rb.velocity = workspace;
        CurrentVelocity = workspace;
    }


    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, (Time.fixedDeltaTime * 54) * velocity);
        Rb.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(
            (Time.fixedDeltaTime * 54) * angle.x * velocity * direction,
            (Time.fixedDeltaTime * 54) * angle.y * velocity
        );
        Rb.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = (Time.fixedDeltaTime * 54) * direction * velocity;
        Rb.velocity = workspace;
        CurrentVelocity = workspace;
    }
    #endregion


    public void CheckIfShouldFlip(float input)
    {
        if (input != 0 && input != FacingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FacingDirection *= -1;
        Rb.transform.Rotate(0, 180.0f, 0);
    }
}
