using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class EnemyController : MonoBehaviour
    {
        public static EnemyController instance;

        public enum Emotions
        {
            Quiet,
            Curious,
            Interested,
            Angry,
            Enraged
        }

        public Emotions currentEmotion = Emotions.Quiet;

        internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        internal SpriteRenderer spriteRenderer;
        [SerializeField] private PlayerController player;

        public float speed;


        public Bounds Bounds => _collider.bounds;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }

            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if(spriteRenderer.isVisible)
            {
                var ev = Schedule<EnemyOnScreen>();
                ev.enemy = this;
                ev.sanity = player.GetComponent<Sanity>();
            }
            else
            {
                var ev = Schedule<EnemyOffScreen>();
                ev.enemy = this;
                ev.sanity = player.GetComponent<Sanity>();
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                var ev = Schedule<PlayerEnemyCollision>();
                ev.player = player;
                ev.enemy = this;
            }
        }

        IEnumerator Movement()
        {
            while(true)
            {
                if(currentEmotion == Emotions.Quiet)
                {

                }
                else if(currentEmotion == Emotions.Curious)
                {

                }
                else if(currentEmotion == Emotions.Interested)
                {

                }
                else if(currentEmotion == Emotions.Angry)
                {

                }
                else if(currentEmotion == Emotions.Enraged)
                {

                }
                yield return new WaitForSeconds(1f);
            }
        }
    }
}