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
        [SerializeField] private float slopeDownAngle = 45.0f;
        [SerializeField] private Vector2 slopeNormalPerp;
        
     
        [SerializeField] private float ledgeCheckDistance;
        [SerializeField] private float wallCheckDistance;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private float slopeCheckDistance;

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
        private bool isOnSlope;
        private bool isFacingRight = true;

        private void Start()
        {
            mainCamera = FindObjectOfType<Camera>();
        }

        private void Update()
        {
            animator.SetFloat("Walking", Mathf.Abs(rb2d.velocity.x));

            GroundCheck();
            WallCheck();
            CoyoteJumpAndBufferTimers();
            
            if (isGrounded && animator.GetBool("Jumping")) animator.SetBool("Jumping", false);
            if (isWallSliding) animator.SetBool("WallSlide", true);
        }

        private void FixedUpdate()
        {
            targetVelocity = velocity * maxSpeed;
            rb2d.velocity = Vector2.MoveTowards(rb2d.velocity, new Vector2(targetVelocity, rb2d.velocity.y),
                acceleration * Time.deltaTime);
            
            if (isOnSlope && velocity != 0)
            {
                cc2d.sharedMaterial = null;
            }
            else
            {
                cc2d.sharedMaterial = new PhysicsMaterial2D { friction = 0.05f };
            }
            FlipX();
        }

        private void LateUpdate()
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

            if (!(Mathf.Abs(velocity) <= .1f)) return;
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

                cc2d.sharedMaterial = slippery_MT;
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
            RaycastHit2D isHit = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckDistance, groundLayerMask);
            Debug.DrawRay(groundCheckPoint.position, Vector2.down, Color.red);

            if (isHit.collider != null) 
            {
                isGrounded = true;
            } else
            {
                isGrounded = false;
            }
            
            if (isGrounded) CheckSlope(); 
        }
        
        private void CheckSlope()
        {
            Vector2 checkPos = transform.position - new Vector3(0.0f, 0.5f);

            RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, groundLayerMask);

            if (hit)
            {
                slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

                slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

                if (slopeDownAngle != 0 && slopeDownAngle <= 75)
                {
                    isOnSlope = true;
                }
                else
                {
                    isOnSlope = false;
                }

                if (isOnSlope && hit.normal.x != 0)
                {
                    rb2d.sharedMaterial = grippy_MT;
                }
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
                
                Debug.DrawRay(wallCheckPoint.position, Vector2.right, Color.green);
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

        private void CoyoteJumpAndBufferTimers()
        {
            // Coyote Time Calculation
            if (isGrounded)
            {
                coyoteTime = Time.time + coyoteDuration;
            } else if (Time.time > coyoteTime)
            {
                isGrounded = false;
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