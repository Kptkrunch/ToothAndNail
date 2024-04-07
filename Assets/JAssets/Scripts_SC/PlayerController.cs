using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JAssets.Scripts_SC
{
    public class PlayerController : MonoBehaviourPunCallbacks
    {
        private bool _isFacingRight = true, canBeHurt = true;
        [SerializeField] private bool _isGrounded, _onSlope, _isJumping, _canWalkOnSlope, _jumpBuffered;
        public Animator animator;
        [SerializeField] private CapsuleCollider2D cc2d;

        private Vector2 colliderSize;
        [SerializeField] private PhysicsMaterial2D grippyMat;
        [SerializeField] private Transform groundDetector;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private InputAction.CallbackContext jumpContext;
        [SerializeField] private float jumpForce, jumpHoldMultiplier;
        [SerializeField] private Camera mainCamera;

        [SerializeField] private float maxSpeed,
            acceleration,
            deceleration,
            velocity,
            targetVelocity,
            slopeSideAngle,
            slopeMaxAngle;

        [SerializeField] private readonly float coyoteTime = 0.2f;
        [SerializeField] private float coyoteTimeTimer;
        [SerializeField] private readonly float jumpBuffer = 0.2f;
        [SerializeField] private float jumpBufferTimer, invulTimer = 1f;

        public int playerNumber;
        [SerializeField] private float rayDistance, slopeDownAngle, slopeDownAngleOld;
        [SerializeField] private GameObject rayOrigin;
        public Rigidbody2D rb2d;
        [SerializeField] private PhysicsMaterial2D slipperyMat;
        [SerializeField] private Vector2 slopeNormalPerpendicular;

        private void Start()
        {
            colliderSize = cc2d.size;
            mainCamera = FindObjectOfType<Camera>();
        }

        private void Update()
        {
            GroundCheck();
            SlopeCheck();
            if (_jumpBuffered) jumpBufferTimer -= Time.deltaTime;
            if (canBeHurt == false) invulTimer -= Time.deltaTime;
        }

        private void FixedUpdate()
        {
            if (photonView.IsMine)
            {
                CoyoteJumpBufferTimers();
                if (_jumpBuffered) jumpBufferTimer -= Time.deltaTime;

                animator.SetFloat("Walking", Mathf.Abs(rb2d.velocity.x));
                animator.SetBool("Grounded", _isGrounded);

                if (_isGrounded && animator.GetBool("Jumping")) animator.SetBool("Jumping", false);

                targetVelocity = velocity * maxSpeed;
                rb2d.velocity = Vector2.MoveTowards(rb2d.velocity, new Vector2(targetVelocity, rb2d.velocity.y),
                    acceleration * Time.deltaTime);

                FlipX();

                if (Mathf.Abs(velocity) < 0.01f)
                    rb2d.velocity = Vector2.MoveTowards(rb2d.velocity, Vector2.zero, deceleration * Time.deltaTime);
            }
        }

        private void LateUpdate()
        {
            if (invulTimer <= 0) canBeHurt = true;
            if (photonView.IsMine)
            {
                mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
                if (Mathf.Abs(velocity) <= .01f) animator.SetFloat("Walking", 0);
            }
        }

        // =================>
        // Move Logic
        // =================>

        public void Move(InputAction.CallbackContext context)
        {
            velocity = context.ReadValue<Vector2>().x;
            var newVelocity = new Vector2(velocity, rb2d.velocity.y);

            if (_isGrounded && !_onSlope && !_isJumping)
            {
                newVelocity.Set(maxSpeed * velocity, 0.0f);
                rb2d.velocity = newVelocity;
            }
            else if (_isGrounded && _onSlope && !_isJumping && _canWalkOnSlope)
            {
                newVelocity.Set(maxSpeed * slopeNormalPerpendicular.x * -velocity,
                    maxSpeed * slopeNormalPerpendicular.y * -velocity);
                rb2d.velocity = newVelocity;
            }
            else if (!_isGrounded)
            {
                targetVelocity = velocity * maxSpeed;
                rb2d.velocity = Vector2.MoveTowards(rb2d.velocity, new Vector2(targetVelocity, rb2d.velocity.y),
                    acceleration * Time.deltaTime);
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

        // =================>
        // Slope Logic
        // =================>
        private void SlopeCheck()
        {
            Vector2 positionCheck = rayOrigin.transform.position - new Vector3(0.0f, colliderSize.y / 2);
            VerticalCheck(positionCheck);
            HorizontalCheck(positionCheck);
        }

        private void HorizontalCheck(Vector2 checkPosition)
        {
            var slopeHitFront = Physics2D.Raycast(checkPosition, transform.right, rayDistance, groundMask);
            var slopeHitBack = Physics2D.Raycast(checkPosition, -transform.right, rayDistance, groundMask);

            if (slopeHitFront)
            {
                _onSlope = true;
                slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
            }
            else if (slopeHitBack)
            {
                _onSlope = true;
                slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
            }
            else
            {
                slopeSideAngle = 0.0f;
                _onSlope = false;
            }
        }

        private void VerticalCheck(Vector2 checkPosition)
        {
            var hit = Physics2D.Raycast(checkPosition, Vector2.down, rayDistance, groundMask);
            if (hit)
            {
                slopeNormalPerpendicular = Vector2.Perpendicular(hit.normal).normalized;
                slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

                if (slopeDownAngle != slopeDownAngleOld) _onSlope = true;

                slopeDownAngleOld = slopeDownAngle;

                Debug.DrawRay(hit.point, hit.normal, Color.green);
                Debug.DrawRay(hit.point, slopeNormalPerpendicular, Color.red);
            }

            if (slopeDownAngle > slopeMaxAngle || slopeSideAngle > slopeMaxAngle)
                _canWalkOnSlope = false;
            else
                _canWalkOnSlope = true;

            if (_onSlope && velocity == 0.0f && _canWalkOnSlope)
                cc2d.sharedMaterial = grippyMat;
            else
                cc2d.sharedMaterial = slipperyMat;
        }

        // =================>
        // Jump Logic
        // =================>

        private void GroundCheck()
        {
            _isGrounded = Physics2D.OverlapCircle(groundDetector.position, 0.1f, groundMask);
            if (rb2d.velocity.y <= 0.0f)
            {
                _isJumping = false;
                animator.SetBool("Jumping", false);
            }
        }

        public void Jump(InputAction.CallbackContext context)
        {
            _jumpBuffered = false;
            if (context.started) jumpBufferTimer = jumpBuffer;


            // base jump logic
            if (context.started && (coyoteTimeTimer > 0 || _isGrounded))
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                animator.SetBool("Jumping", true);
                _isJumping = true;
            }

            // pre landing jump buffering
            if (context.started && coyoteTimeTimer > 0 && jumpBufferTimer > 0)
            {
                jumpBufferTimer = 0;
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                animator.SetBool("Jumping", true);
                _isJumping = true;
            }

            // hold longer to jump higher
            if (!_isGrounded && context.canceled && rb2d.velocity.y > 0.0f)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * jumpHoldMultiplier);
                _isGrounded = false;
                _jumpBuffered = true;
                coyoteTimeTimer = 0;
            }
        }

        private void CoyoteJumpBufferTimers()
        {
            if (_isGrounded)
            {
                rb2d.gravityScale = 0;
                coyoteTimeTimer = coyoteTime;
            }
            else
            {
                rb2d.gravityScale = 5;
                coyoteTimeTimer -= Time.deltaTime;
            }
        }
    }
}