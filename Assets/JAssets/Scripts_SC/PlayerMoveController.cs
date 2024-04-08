using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JAssets.Scripts_SC
{
    public class PlayerMoveController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float velocity;
        [SerializeField] private float targetVelocity;
        [SerializeField] private float acceleration;
        [SerializeField] private float deceleration;
        
        [SerializeField] private float massFactor = 0.05f;

        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private readonly float jumpHoldMultiplier = 0.5f;
        [SerializeField] private float wallSlideSpeed = 0.5f;
        [SerializeField] private float wallJumpForce = 10f;
        [SerializeField] private Vector2 wallJumpDirection = new(1f, 2f);
        
        [SerializeField] private float ledgeClimbSpeed = 1f;
        [SerializeField] private float maxSlopeAngle = 45.0f;
     
        [SerializeField] private float ledgeCheckDistance;
        [SerializeField] private float wallCheckDistance;
        [SerializeField] private float groundCheckDistance;

        [SerializeField] private Transform wallCheckPoint;
        [SerializeField] private Transform ledgeCheckPoint;
        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private Transform groundCheckPointOrigin;
        
        
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private LayerMask wallLayerMask;


        [SerializeField] private float coyoteDuration = 0.15f; // Duration for Coyote Time
        private float coyoteTime;
        [SerializeField] private float jumpBufferLength = 0.15f; // Duration for Jump Buffer
        private float jumpBufferCount;

        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private CapsuleCollider2D cc2d;
        [SerializeField] private Animator animator;
        [SerializeField] private Camera mainCamera;
        
        [SerializeField] private PhysicsMaterial2D slippery_MT;
        [SerializeField] private PhysicsMaterial2D grippy_MT;
        
        private bool canLedgeHang;
        private bool isCrouching;
        private bool isOnLedge;
        private bool isGrounded;
        private bool isTouchingWall;
        private bool isWallSliding;
        private bool isFacingRight = true;

        private void Start()
        {
            mainCamera = FindObjectOfType<Camera>();
        }

        private void Update()
        {
            GroundCheck();
            WallCheck();
        }

        private void FixedUpdate()
        {
            CoyoteJumpAndBufferTimers();

            animator.SetFloat("Walking", Mathf.Abs(rb2d.velocity.x));
            animator.SetBool("Grounded", isGrounded);
            if (isGrounded && animator.GetBool("Jumping")) animator.SetBool("Jumping", false);
            if (isWallSliding) animator.SetBool("WallSlide", true);

            targetVelocity = velocity * maxSpeed;
            rb2d.velocity = Vector2.MoveTowards(rb2d.velocity, new Vector2(targetVelocity, rb2d.velocity.y),
                acceleration * Time.deltaTime);

            FlipX();
        }

        private void LateUpdate()
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

            if (!(Mathf.Abs(velocity) <= .01f)) return;
            rb2d.velocity = Vector2.MoveTowards(rb2d.velocity, Vector2.zero, deceleration * Time.deltaTime);
            animator.SetFloat("Walking", 0);
            
        }

        public void Move(InputAction.CallbackContext context)
        {
            if (isGrounded) {
                velocity = context.ReadValue<Vector2>().x;
                var newVelocity = new Vector2(velocity, rb2d.velocity.y);
                newVelocity.Set(maxSpeed * velocity, 0.0f);
                rb2d.velocity = newVelocity;
            } else if (!isGrounded)
            {
                targetVelocity = velocity * maxSpeed;
                rb2d.velocity = Vector2.MoveTowards(rb2d.velocity, new Vector2(targetVelocity, rb2d.velocity.y),
                    acceleration * Time.deltaTime);
            }
        }
        
        public void Jump(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (isGrounded || Time.time < coyoteTime || jumpBufferCount > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                isGrounded = false;
                coyoteTime = Time.time;
                animator.SetBool("Jumping", true);
            } else
            {
                jumpBufferCount = jumpBufferLength;
            }

            if (context.canceled && !isGrounded && rb2d.velocity.y > 0.0f)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * jumpHoldMultiplier);
            }
        }
        
        public void Crouch(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                isCrouching = true;
                animator.SetBool("Crouch", true);
            }

            if (!context.canceled) return;
            isCrouching = false;
            animator.SetBool("Crouch", false);
        }
        
        private void GroundCheck()
        {
            RaycastHit2D isHit = Physics2D.Raycast(groundCheckPointOrigin.position, Vector2.down, groundCheckDistance, groundLayerMask);
            if (isHit.collider != null) 
            {
                isGrounded = true;
            } 
            else
            {
                isGrounded = false;
            }
        }

        private void WallCheck()
        {
            var direction = isFacingRight ? Vector2.right : Vector2.left;
            RaycastHit2D wallCheckHit = Physics2D.Raycast(wallCheckPoint.position, direction, wallCheckDistance, wallLayerMask);            
            if (wallCheckHit.collider != null)
            {
                isWallSliding = true;
                WallSlide();
                
                RaycastHit2D ledgeCheckHit = Physics2D.Raycast(ledgeCheckPoint.position, Vector2.right, ledgeCheckDistance, wallLayerMask);
                if (ledgeCheckHit.collider == null && isTouchingWall)
                {
                    canLedgeHang = true;
                } else
                {
                    canLedgeHang = false;
                }
            }
        }

        
        private void FlipX()
        {
            if (velocity > 0 && !isFacingRight)
                FlipCharacter();
            else if (velocity < 0 && isFacingRight)
                FlipCharacter();
        }

        private void FlipCharacter()
        {
            isFacingRight = !isFacingRight;
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }

        // This will rotate a vector by a given angle
        // private static Vector2 RotateVectorb2dyAngle(Vector2 vector, float angle)
        // {
        //     var radian = angle * Mathf.Deg2Rad;
        //
        //     var _x = vector.x * Mathf.Cos(radian) - vector.y * Mathf.Sin(radian);
        //     var _y = vector.x * Mathf.Sin(radian) + vector.y * Mathf.Cos(radian);
        //
        //     return new Vector2(_x, _y);
        // }

        // public void LedgeGrab(InputAction.CallbackContext context)
        // {
        //     // Check for ledge grabbing conditions (reusing WallSlide logic)
        //
        //     // Check if the player can hang and if they're wall sliding
        //     if (canLedgeHang && isWallSliding)
        //     {
        //         if (context.performed)
        //         {
        //             // Freeze y velocity to essentially 'grab'
        //             rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
        //             isOnLedge = true;
        //             animator.SetBool("OnLedge", true);
        //         }
        //     }
        //     else if (isOnLedge && !canLedgeHang)
        //     {
        //         // If moving from a hang to a slide
        //         isOnLedge = false;
        //         animator.SetBool("OnLedge", false);
        //     }
        // }

        // private IEnumerator LedgeClimb(InputAction.CallbackContext context)
        // {
        //     yield return LedgeClimb(context);
        //     Vector3 targetPos = new Vector2(ledgeCheckPoint.position.x, ledgeCheckPoint.position.y + ledgeClimbSpeed);
        //     while (Vector3.Distance(transform.position, targetPos) > 0.05)
        //     {
        //         transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * ledgeClimbSpeed);
        //         yield return null;
        //     }
        //     isOnLedge = false;
        // }

        private void CoyoteJumpAndBufferTimers()
        {
            // Coyote Time Calculation
            if (isGrounded)
            {
                coyoteTime = Time.time + coyoteDuration;
                rb2d.gravityScale = 0;
            } else if (Time.time > coyoteTime)
            {
                isGrounded = false;
                rb2d.gravityScale = 5;
            }
            
            if (jumpBufferCount > 0)
            {
                jumpBufferCount -= Time.deltaTime;
            }
        }
        private void WallSlide()
        {
            if (isTouchingWall && !isOnLedge)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, -wallSlideSpeed);
                isWallSliding = true;
            }
            else
            {
                isWallSliding = false;
            }
        }

        public void WallJump(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (isWallSliding || isTouchingWall)
            {
                var forceAdd = new Vector2(wallJumpDirection.x * transform.localScale.x, wallJumpDirection.y) *
                               wallJumpForce;
                rb2d.AddForce(forceAdd, ForceMode2D.Impulse);
                animator.SetBool("WallJump", true);
            }
            
            if (context.canceled) animator.SetBool("WallJump", false);
        }

        public void Attack(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            
            animator.SetBool("Attack", true);
        }
    }
}