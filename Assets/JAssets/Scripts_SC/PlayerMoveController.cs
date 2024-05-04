using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JAssets.Scripts_SC
{
    public class PlayerMoveController : MonoBehaviourPunCallbacks
    {
        public int playerNumber;
        public string playerTag;
        public bool isPlayer;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float velocity;
        [SerializeField] private float targetVelocity;
        [SerializeField] private float acceleration;
        [SerializeField] private float deceleration;
        
        [SerializeField] private float massFactor = 0.05f;

        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float jumpHoldMultiplier = 0.5f;
        [SerializeField] private float wallSlideSpeed = 0.5f;
        [SerializeField] private float wallJumpForce = 10f;
        [SerializeField] private Vector2 wallJumpDirection = new(1f, 2f);
        
        [SerializeField] private float ledgeClimbSpeed = 1f;
        [SerializeField] private Vector2 slopeNormalPerp;
     
        [SerializeField] private float ledgeCheckDistance;
        [SerializeField] private float wallCheckDistance;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private float slopeCheckDistance;

        [SerializeField] private Transform wallCheckPoint;
        [SerializeField] private Transform ledgeCheckPoint;
        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private Transform viewPoint;
        public GameObject coneOfVision;

        
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private LayerMask wallLayerMask;


        [SerializeField] private float coyoteTime = 0.15f;
        private float coyoteTimeTimer;
        [SerializeField] private float preLandingJumpBuffer = 0.15f;
        private float jumpBufferTimer;

        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private CapsuleCollider2D cc2d;
        [SerializeField] private BoxCollider2D bc2d;
        [SerializeField] private Animator animator;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private PlayerHealthController healthController;
        private InputActionAsset inputActionAsset;
        private InputActionMap player;

        private Vector2 rightStick;
        
        [SerializeField] private PhysicsMaterial2D slippery_MT;
        [SerializeField] private PhysicsMaterial2D grippy_MT;

        private bool isFacingRight = true;
        private bool isCrouching;
        private bool isOnLedge;
        private bool isGrounded;
        private bool isTouchingWall;
        private bool isWallSliding;
        private bool isOnSlope;
        private bool canWalkOnSlope;
        private bool canLedgeHang;
        private static readonly int Slide = Animator.StringToHash("WallSlide");
        private static readonly int Walking = Animator.StringToHash("Walking");
        private static readonly int Jumping = Animator.StringToHash("Jumping");
        private static readonly int Crouch1 = Animator.StringToHash("Crouch");
        private static readonly int Grounded = Animator.StringToHash("Grounded");
        private static readonly int WallJump1 = Animator.StringToHash("WallJump");

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            playerCamera = GetComponentInChildren<Camera>();
        }

        private void Start()
        {
            DataTypeController.PlayerNumAndTagData playerData;
            playerData = PlayerList.instance.AddPlayerToList(gameObject);
            playerNumber = playerData.PlayerNumber;
            playerTag = playerData.PlayerTag;
            coneOfVision.GetComponentInChildren<LookRotate>().tag = playerData.PlayerTag;
        }

        private void Update()
        {
            if (isWallSliding) animator.SetBool(Slide, true);
        
            targetVelocity = velocity * maxSpeed;
            rb2d.velocity = Vector2.MoveTowards(rb2d.velocity, new Vector2(targetVelocity, rb2d.velocity.y),
                acceleration * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            GroundCheck();
            NewSlopeCheck();
            WallCheck();
            CoyoteJumpAndBufferTimers();
            FlipX();
        }

        private void LateUpdate()
        {
            coneOfVision.gameObject.transform.position = viewPoint.position;
            // Vector3 desiredPosition = new Vector3(transform.position.x, transform.position.y + 0.25f, -10);
            // playerCamera.gameObject.transform.position = desiredPosition;
            if (!(Mathf.Abs(velocity) <= 0.1f)) return;
            if (isCrouching) rb2d.velocity = Vector2.zero;
            
            rb2d.velocity = Vector2.MoveTowards(rb2d.velocity, Vector2.zero, deceleration * Time.deltaTime);
            animator.SetFloat(Walking, 0);
        }

        public void Move(InputAction.CallbackContext context)
        {
            if (!context.performed || isCrouching || healthController.currentHealth <= 0) return;
            float currentAcceleration;

            if (isGrounded)
            {
                animator.SetFloat(Walking, Mathf.Abs(rb2d.velocity.x));
                currentAcceleration = acceleration;
                velocity = context.ReadValue<Vector2>().x;
                
                targetVelocity = velocity * maxSpeed;
                rb2d.velocity = Vector2.MoveTowards(rb2d.velocity, new Vector2(targetVelocity, rb2d.velocity.y),
                    currentAcceleration * Time.deltaTime);
            } else if (!isGrounded)
            {
                animator.SetFloat(Walking, Mathf.Abs(rb2d.velocity.x));
                currentAcceleration = acceleration * 0.5f;
                velocity = context.ReadValue<Vector2>().x;

                targetVelocity = velocity * maxSpeed;
                rb2d.velocity = Vector2.MoveTowards(rb2d.velocity, new Vector2(targetVelocity, rb2d.velocity.y),
                    currentAcceleration * Time.deltaTime);
                cc2d.sharedMaterial = slippery_MT;
            }

        }
        
        public void Jump(InputAction.CallbackContext context)
        {
            if (!context.performed || healthController.currentHealth <= 0) return;
            
            if (isGrounded || coyoteTimeTimer > 0 || jumpBufferTimer > 0)
            {
                coyoteTime = 0;
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                animator.SetBool(Jumping, true);
            }
            else if (rb2d.velocity.y < 0 && context.canceled && !isGrounded) 
            {
                jumpBufferTimer = preLandingJumpBuffer;
            } 
            else if (context.performed && jumpBufferTimer > 0)
            {
                jumpBufferTimer = 0;
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                animator.SetBool(Jumping, true);
            }
            
            if (context.canceled && !isGrounded && rb2d.velocity.y > 0.0f)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * jumpHoldMultiplier);
                coyoteTime = 0;
            }
        }
        
        public void Crouch(InputAction.CallbackContext context)
        {
            if (healthController.currentHealth <= 0) return;
            if (context.performed && isGrounded)
            {
                isCrouching = true;
                animator.SetBool(Crouch1, true);
                rb2d.velocity = Vector2.zero;
                targetVelocity = 0;
            }

            if (context.canceled)
            {
                isCrouching = false;
                animator.SetBool(Crouch1, false);
            }

        }
        
        private void GroundCheck()
        {
            RaycastHit2D hit = Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.down, 0.1f,
                groundLayerMask);
            
            if (hit.collider) 
            {
                isGrounded = true;
                animator.SetBool(Grounded, true);
            } else
            {
                isGrounded = false;
                animator.SetBool(Grounded, false);
            }
        }

        private void NewSlopeCheck()
        {
            // Slope check
            RaycastHit2D hitLeft = Physics2D.Raycast(bc2d.bounds.min, Vector2.down, 0.1f, groundLayerMask);
            RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(bc2d.bounds.max.x, bc2d.bounds.min.y), Vector2.down,
                0.1f, groundLayerMask);
            
            if (isGrounded && (hitLeft.collider || hitRight.collider))
            {
                Vector2 averageNormal = (hitLeft.normal + hitRight.normal).normalized;
                Vector2 directionOnSlope = Vector2.Perpendicular(averageNormal).normalized;

                // Choose the perpendicular direction that matches our input
                if ((velocity < 0 && directionOnSlope.x > 0) || (velocity > 0 && directionOnSlope.x < 0))
                    directionOnSlope *= -1;

                rb2d.velocity = directionOnSlope * (moveSpeed * Mathf.Abs(velocity));
            }
        }

        private void LedgeCheck()
        {
            RaycastHit2D ledgeCheckHit = Physics2D.Raycast(ledgeCheckPoint.position, Vector2.right, ledgeCheckDistance, wallLayerMask);
            Debug.DrawRay(ledgeCheckPoint.position, Vector2.right, Color.yellow);

            if (!ledgeCheckHit.collider && isTouchingWall)
            {
                canLedgeHang = true;
            } else
            {
                canLedgeHang = false;
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
            if (isGrounded)
            {
                coyoteTimeTimer = coyoteTime;
            } else if (coyoteTime < 0)
            {
                isGrounded = false;
            }

            if (!isGrounded)
            {
                coyoteTimeTimer -= Time.deltaTime;
            }
            
            if (jumpBufferTimer > 0)
            {
                jumpBufferTimer -= Time.deltaTime;
            }
        }
        
        private void WallCheck()
        {
            var direction = isFacingRight ? Vector2.right : Vector2.left;
            
            RaycastHit2D wallCheckHit = Physics2D.Raycast(wallCheckPoint.position, direction, wallCheckDistance, wallLayerMask);     
            Debug.DrawRay(wallCheckPoint.position, direction, Color.green);

            if (wallCheckHit.collider && !isGrounded)
            {
                isTouchingWall = true;
                WallSlide();
                
                LedgeCheck();
            }
            else
            {
                animator.SetBool(Slide, false);
                isTouchingWall = false;
                isWallSliding = false;
            }
        }
        
        private void WallSlide()
        {
            if (isTouchingWall)
            {
                isWallSliding = true;
                rb2d.velocity = new Vector2(rb2d.velocity.x, -wallSlideSpeed);
            }
            else
            {
                isWallSliding = false;
            }
        }

        public void WallJump(InputAction.CallbackContext context)
        {
            if (!context.performed || healthController.currentHealth <= 0) return;
            if (isWallSliding)
            {
                var forceAdd = new Vector2(
                    wallJumpDirection.x * -transform.localScale.x, wallJumpDirection.y) * wallJumpForce;

                rb2d.velocity = forceAdd;
                animator.SetBool(WallJump1, true);
            }
            
            if (context.canceled || isGrounded) animator.SetBool(WallJump1, false);
        }
    }
}