using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace JAssets.Scripts_SC
{
    // Add up to 4 colliders for predictive ground checkouts. You can add more if need.

    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float massFactor = 0.05f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float crouchHeight = 0.5f;
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

        [SerializeField] private UnityEvent onJump, onCrouch, onLedgeGrab, onWallSlide, onWallJump, onAttack;

        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private CapsuleCollider2D cc;
        [SerializeField] private PhysicsMaterial2D MatSlippery;
        [SerializeField] private PhysicsMaterial2D MatGrippy;
        private bool canHang;
        private float coyoteTime;


        private PlayerInputActions inputActions;

        private bool isCrouching;
        private bool isOnLedge;
        private bool isTouchingGround;
        private bool isTouchingWall;
        private bool isWallSliding;
        private float jumpBufferCount;
        private readonly CustomRaycast[] predictiveGroundCheckRays = new CustomRaycast[5];


        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            inputActions = new PlayerInputActions();

            // Registering input callbacks
            inputActions.Jump.performed += _ => Jump();
            inputActions.Crouch.performed += _ => Crouch();
            inputActions.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
            inputActions.LedgeGrab.performed += _ => LedgeGrab();
            inputActions.WallSlide.performed += _ => WallSlide();
            inputActions.WallJump.performed += _ => WallJump();
            inputActions.Attack.performed += _ => Attack();

            inputActions.Jump.performed += context => OnJumpPerformed();
        }

        private void Update()
        {
            isTouchingWall = Physics2D.OverlapCircle(wallCheckPoint.position, wallCheckRadius, wallLayerMask);
            // Check for the ground
            isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayerMask);

            //...Prior Update Logic...

            // Coyote Time Calculation
            if (isTouchingGround)
                coyoteTime = Time.time + coyoteDuration;
            else if (Time.time > coyoteTime)
                isTouchingGround = false;
            else
                SlopeScan();

            // Jump Buffer Calculation
            if (jumpBufferCount > 0)
                jumpBufferCount -= Time.deltaTime;
            if (isTouchingWall)
            {
                var hit = Physics2D.Raycast(ledgeCheckPoint.position, Vector2.right, ledgeCheckDistance, wallLayerMask);
                if (!hit && isTouchingWall)
                    canHang = true;
                else
                    canHang = false;
            }

            if (isOnLedge) rb.velocity = new Vector2(rb.velocity.x, 0f);

            if (isCrouching)
                transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);
        }

        private void OnEnable()
        {
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        private void Move(Vector2 direction)
        {
            rb.velocity = new Vector2(direction.x * moveSpeed - massFactor * rb.mass, rb.velocity.y);
        }

        private void SlopeScan()
        {
            foreach (GroundCastDirection direction in Enum.GetValues(typeof(GroundCastDirection)))
            {
                var endPosition = RotateVectorByAngle(Vector2.down, (float)direction) * cc.size.y * 0.5f;
                var groundHit = Physics2D.Raycast(transform.position, endPosition, 1, groundLayerMask);

                if (groundHit.collider)
                {
                    // Check if the ground angle is on or below the slope limit.
                    var groundAngle = Mathf.Abs(Vector2.Angle(groundHit.normal, Vector2.up));

                    if (groundHit.collider is CompositeCollider2D || groundAngle <= maxSlopeAngle)
                        cc.sharedMaterial = MatSlippery;

                    else
                        cc.sharedMaterial = MatGrippy;
                }

                predictiveGroundCheckRays[(int)direction] = new CustomRaycast(groundHit, direction);
            }
        }

        // This will rotate a vector by a given angle
        public static Vector2 RotateVectorByAngle(Vector2 vector, float angle)
        {
            var radian = angle * Mathf.Deg2Rad;

            var _x = vector.x * Mathf.Cos(radian) - vector.y * Mathf.Sin(radian);
            var _y = vector.x * Mathf.Sin(radian) + vector.y * Mathf.Cos(radian);

            return new Vector2(_x, _y);
        }

        private void Jump()
        {
            if (isTouchingGround || Time.time < coyoteTime)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                onJump?.Invoke();
                isTouchingGround = false;
                coyoteTime = Time.time;
            }
            else
            {
                jumpBufferCount = jumpBufferLength;
            }
        }

        private void Crouch()
        {
            isCrouching = !isCrouching;
            onCrouch?.Invoke();
        }

        private void LedgeGrab()
        {
            // Check for ledge grabbing conditions (reusing WallSlide logic)
            WallSlide();

            // Check if the player can hang and if they're wall sliding
            if (canHang && isWallSliding)
            {
                // Freeze y velocity to essentially 'grab'
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                isOnLedge = true;
                onLedgeGrab?.Invoke();
            }
            else if (isOnLedge && !canHang)
            {
                // If moving from a hang to a slide
                isOnLedge = false;
            }
        }

        private IEnumerator LedgeClimb()
        {
            Vector3 targetPos = new Vector2(ledgeCheckPoint.position.x, ledgeCheckPoint.position.y + ledgeClimbSpeed);
            while (Vector3.Distance(transform.position, targetPos) > 0.05)
            {
                transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * ledgeClimbSpeed);
                yield return null;
            }

            isOnLedge = false;
        }

        private void OnJumpPerformed()
        {
            if (isOnLedge)
            {
                onJump?.Invoke();
                StartCoroutine(LedgeClimb());
            }
        }

        private void WallSlide()
        {
            if (isTouchingWall && !isOnLedge)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
                isWallSliding = true;
                onWallSlide?.Invoke();
            }
            else
            {
                isWallSliding = false;
            }
        }

        private void WallJump()
        {
            if (isWallSliding || isTouchingWall)
            {
                var forceAdd = new Vector2(wallJumpDirection.x * transform.localScale.x, wallJumpDirection.y) *
                               wallJumpForce;
                rb.AddForce(forceAdd, ForceMode2D.Impulse);
            }

            onWallJump?.Invoke();
        }

        private void Attack()
        {
            // Implement attack logic here
            onAttack?.Invoke();
        }
    }
}