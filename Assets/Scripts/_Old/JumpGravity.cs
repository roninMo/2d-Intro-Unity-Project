using UnityEngine;

public class JumpGravity : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float downardJumpGravity = 10f;
    [SerializeField] private float upwardJumpGravity = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Jump gravity logic
        if (rb.velocity.y < 0) // Downwards gravity
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (downardJumpGravity - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) // Upwards Gravity
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (upwardJumpGravity - 1) * Time.fixedDeltaTime;
        }
    }
}
