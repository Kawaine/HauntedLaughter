using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using Sirenix.OdinInspector;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public static PlayerController instance = null;

        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float speed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Sanity sanity;
        public bool controlEnabled = true;
        [MinMaxSlider(-2, 0)] public Vector2 yMovementRange = Vector2.zero;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        internal Rigidbody2D rigbodRef;
        private Vector2 moveVelocity;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }

            sanity = GetComponent<Sanity>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            rigbodRef = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                //move.x = Input.GetAxis("Horizontal");
                Move();
                /*if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }*/
            }
            else
            {
                move.x = 0;
            }
            //UpdateJumpState();
            //base.Update();
        }

        void Move()
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveVelocity = moveInput.normalized;
            if(moveVelocity.x > 0)
            {
                animator.SetBool("WalkLeft", false);
                animator.SetBool("WalkRight", true);
                animator.SetBool("WalkForward", false);
                animator.SetBool("WalkBack", false);
            }
            else if(moveVelocity.x < 0)
            {
                animator.SetBool("WalkLeft", true);
                animator.SetBool("WalkRight", false);
                animator.SetBool("WalkForward", false);
                animator.SetBool("WalkBack", false);
            }
            else if(moveVelocity.y < 0)
            {
                animator.SetBool("WalkLeft", false);
                animator.SetBool("WalkRight", false);
                animator.SetBool("WalkForward", true);
                animator.SetBool("WalkBack", false);
            }
            else if(moveVelocity.y > 0)
            {
                animator.SetBool("WalkLeft", false);
                animator.SetBool("WalkRight", false);
                animator.SetBool("WalkForward", false);
                animator.SetBool("WalkBack", true);
            }
            else if(moveVelocity == Vector2.zero)
            {
                animator.SetBool("WalkLeft", false);
                animator.SetBool("WalkRight", false);
                animator.SetBool("WalkForward", false);
                animator.SetBool("WalkBack", false);
            }
            moveVelocity *= speed;
        }

        protected override void FixedUpdate()
        {
            rigbodRef.MovePosition(rigbodRef.position + moveVelocity * Time.deltaTime);
        }


        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / speed);

            targetVelocity = move * speed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}