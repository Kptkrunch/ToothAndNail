using System;
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
        [SerializeField] private float wallSlideSpeed = 0.5f;
        [SerializeField] private float wallJumpForce = 10f;
        [SerializeField] private float ledgeClimbSpeed = 1f;
        [SerializeField] private float maxSlopeAngle = 45.0f;
        [SerializeField] private Vector2 wallJumpDirection = new(1f, 2f);
        [SerializeField] private Transform wallCheckPoint;
        [SerializeField] private Transform ledgeCheckPoint;
        [SerializeField] private float ledgeCheckDistance;
        [SerializeField] private float wallCheckRadius;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private LayerMask wallLayerMask;
        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private LayerMask groundLayerMask;

        [SerializeField] private float coyoteDuration = 0.15f; // Duration for Coyote Time
        [SerializeField] private float jumpBufferLength = 0.15f; // Duration for Jump Buffer
        
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private CapsuleCollider2D cc;
        [SerializeField] private Animator animator;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private PhysicsMaterial2D MatSlippery;
        [SerializeField] private PhysicsMaterial2D MatGrippy;
        private bool canLedgeHang;
        private float coyoteTime;
        
        private bool isCrouching;
        private bool isOnLedge;
        private bool isGrounded;
        private bool isTouchingWall;
        private bool isWallSliding;
        private float jumpBufferCount;
        private readonly CustomRaycast[] predictiveGroundCheckRays = new CustomRaycast[5];
        private bool _isFacingRight = true;
        private bool isMovementInput;
        private readonly float jumpHoldMultiplier = 0.5f;

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

            animator.SetFloat("Walking", Mathf.Abs(rb.velocity.x));
            animator.SetBool("Grounded", isGrounded);
            if (isGrounded && animator.GetBool("Jumping")) animator.SetBool("Jumping", false);
            if (isWallSliding) animator.SetBool("WallSlide", true);

            targetVelocity = velocity * maxSpeed;
            rb.velocity = Vector2.MoveTowards(rb.velocity, new Vector2(targetVelocity, rb.velocity.y),
                acceleration * Time.deltaTime);

            FlipX();
        }

        private void LateUpdate()
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

            if (!(Mathf.Abs(velocity) <= .01f)) return;
            rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, deceleration * Time.deltaTime);
            animator.SetFloat("Walking", 0);
            
        }

        public void Move(InputAction.CallbackContext context)
        {
            velocity = context.ReadValue<Vector2>().x;
            var newVelocity = new Vector2(velocity, rb.velocity.y);
            newVelocity.Set(maxSpeed * velocity, 0.0f);
            rb.velocity = newVelocity;
        }
        
        public void Jump(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (isGrounded || Time.time < coyoteTime)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                isGrounded = false;
                coyoteTime = Time.time;
                animator.SetBool("Jumping", true);
            } else
            {
                jumpBufferCount = jumpBufferLength;
            }

            if (context.canceled && !isGrounded && rb.velocity.y > 0.0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpHoldMultiplier);
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
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.1f, groundLayerMask);
            if (rb.velocity.y <= 0.0f)
            {
                isGrounded = false;
                animator.SetBool("Jumping", false);
            }
        }

        private void WallCheck()
        {
            isTouchingWall = Physics2D.OverlapCircle(wallCheckPoint.position, wallCheckRadius, wallLayerMask);
            if (isTouchingWall)
            {
                isWallSliding = true;
                WallSlide();
                
                var hit = Physics2D.Raycast(ledgeCheckPoint.position, Vector2.right, ledgeCheckDistance, wallLayerMask);
                if (!hit && isTouchingWall)
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
            if (velocity > 0 && !_isFacingRight)
                FlipCharacter();
            else if (velocity < 0 && _isFacingRight)
                FlipCharacter();
        }

        private void FlipCharacter()
        {
            _isFacingRight = !_isFacingRight;
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }

        // This will rotate a vector by a given angle
        // private static Vector2 RotateVectorByAngle(Vector2 vector, float angle)
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
        //             rb.velocity = new Vector2(rb.velocity.x, 0f);
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
                rb.gravityScale = 0;
            } else if (Time.time > coyoteTime)
            {
                isGrounded = false;
                rb.gravityScale = 5;
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
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
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
                rb.AddForce(forceAdd, ForceMode2D.Impulse);
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