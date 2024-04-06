using UnityEngine;

public class PlayerControllerAlt : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    public float deceleration;
    public float jumpForce;
    public float wallJumpForce;
    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;

    private float groundAngle;
    private bool isJumping;
    private bool isOnGround;
    private bool isFacingRight = true;
    private bool isWallSliding;
    private Vector2 direction;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        GetInput();
        ApplyAcceleration();
        CheckGround();
        CheckWall();
        Jump();
        Flip();
        CapSpeed();
        SlopeAdjustment();
    }

    void GetInput()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void ApplyAcceleration()
    {
        if(isOnGround)
        {
            if (direction.x != 0)
            {
                rb2d.AddForce(new Vector2(direction.x * acceleration, 0));
            }
            else if (direction.x == 0 && rb2d.velocity.x != 0)
            {
                rb2d.velocity -= new Vector2(deceleration * Mathf.Sign(rb2d.velocity.x), 0);
            }
        }
        
        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
        {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        }
    }

    void CheckGround()
    {
        isOnGround = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, whatIsGround);

        if (isOnGround)
        {
            isJumping = false;
            
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, whatIsGround);
            groundAngle = Vector2.Angle(hit.normal, Vector2.up);
        }
    }

    void CheckWall()
    {
        isWallSliding = Physics2D.Raycast(wallCheck.position, transform.right, 0.5f, whatIsWall);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            if (isOnGround)
            {
                rb2d.AddForce(new Vector2(0, jumpForce));
                isJumping = true;
            }

            if (isWallSliding && !isOnGround)
            {
                rb2d.velocity = new Vector2(-direction.x * wallJumpForce, wallJumpForce);
                isJumping = true;
            }
        }
    }

    void Flip()
    {
        if ((rb2d.velocity.x > 0 && !isFacingRight) || (rb2d.velocity.x < 0 && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    
    void CapSpeed()
    {
        rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed), rb2d.velocity.y);
    }
    
    void SlopeAdjustment()
    {
        if (isOnGround)
        {
            float velocityY = groundAngle == 0 ? 0 : rb2d.velocity.y;
            rb2d.velocity = new Vector2(rb2d.velocity.x, velocityY - groundAngle / 90);
        }
    }
}