using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;
using DG.Tweening;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class EnemyController : MonoBehaviour
    {
        public static EnemyController instance;
        public delegate Coroutine action(bool actionOccurring);

        public enum Emotions
        {
            Asleep,
            Bored,
            Quiet,
            Curious,
            Interested,
            Angry,
            Enraged
        }

        private Emotions currentEmotion = Emotions.Asleep;

        public Emotions currentEmotion_
        {
            get
            {
                return currentEmotion;
            }
            set
            {
                currentEmotion = value;
                if (currentEmotion == Emotions.Bored)
                {
                    chanceToAppearOffScreen = 0;
                    //chanceToAppearOnScreen = 0;
                    chanceToScare = 100;
                }
                else if (currentEmotion == Emotions.Quiet)
                {
                    chanceToAppearOffScreen = 100;
                    //chanceToAppearOnScreen = 100;
                    chanceToScare = 0;
                }
                else if (currentEmotion == Emotions.Curious)
                {
                    chanceToAppearOffScreen = 40;
                    //chanceToAppearOnScreen = 20;
                    chanceToScare = 0;
                }
                else if (currentEmotion == Emotions.Interested)
                {
                    chanceToAppearOffScreen = 60;
                    //chanceToAppearOnScreen = 0;
                    chanceToScare = 0;
                }
                else if (currentEmotion == Emotions.Angry)
                {
                    chanceToAppearOffScreen = 0;
                    //chanceToAppearOnScreen = 0;
                    chanceToScare = 0;
                }
                else if (currentEmotion == Emotions.Enraged)
                {
                    chanceToAppearOffScreen = 100;
                    //chanceToAppearOnScreen = 100;
                    chanceToScare = 100;
                }
            }
        }


        internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        internal SpriteRenderer spriteRenderer;
        internal Animator animator;
        [SerializeField] private PlayerController player = null;
        [SerializeField] private GameObject[] offScreenSpawns = null;
        //[SerializeField] private GameObject[] onScreenSpawns = null;

        //private float chanceToAppearOnScreen = 0;
        //private bool recentlyAppearedOnScreen = false;
        private float chanceToAppearOffScreen = 0;
        private bool recentlyAppearedOffScreen = false;
        private float chanceToScare = 0;
        private bool recentlyScared = false;

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
            animator = GetComponent<Animator>();
            animator.enabled = false;
            _collider = GetComponent<Collider2D>();
            _collider.enabled = false;
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            Disappear();
            StartCoroutine(Action());
        }

        private void Update()
        {
            if(spriteRenderer.isVisible)
            {
                player.GetComponent<Sanity>().withinRangeOfEnemy_ = true;

            }
            else
            {
                player.GetComponent<Sanity>().withinRangeOfEnemy_ = false;
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

        IEnumerator Action()
        {
            //float onScreenChance = 0;
            float offScreenChance = 0;
            float scareChance = 0;
            while(true)
            {
                if(currentEmotion == Emotions.Bored)
                {
                    yield return new WaitForSeconds(Random.Range(6f, 9f));
                }
                else if(currentEmotion == Emotions.Quiet)
                {
                    yield return new WaitForSeconds(Random.Range(5.5f, 8f));
                }
                else if(currentEmotion == Emotions.Curious)
                {
                    yield return new WaitForSeconds(Random.Range(5f, 7f));
                }
                else if(currentEmotion == Emotions.Interested)
                {
                    yield return new WaitForSeconds(Random.Range(4f, 6f));
                }
                else if(currentEmotion == Emotions.Angry)
                {
                    yield return new WaitForSeconds(Random.Range(3.5f, 5f));
                }
                else if(currentEmotion == Emotions.Enraged)
                {
                    yield return new WaitForSeconds(Random.Range(2.5f, 4f));
                }
                if(currentEmotion != Emotions.Asleep)
                {
                    //onScreenChance = Random.Range(0, 100);
                    offScreenChance = Random.Range(0, 100);
                    scareChance = Random.Range(0, 100);
                    if (scareChance <= chanceToScare && !recentlyScared)
                    {
                        Scare();
                    }
                    else if (offScreenChance <= chanceToAppearOffScreen && !recentlyAppearedOffScreen)
                    {
                        StartCoroutine(AppearOffScreen());
                        yield return new WaitUntil(() => spriteRenderer.enabled == false);
                        Debug.Log("disappeared");
                    }
                }
                yield return null;
            }
        }

        private void Disappear()
        {

        }

        /*private IEnumerator AppearOnScreen()
        {
            Debug.Log("Appeared Off Screen");
            recentlyAppearedOnScreen = true;
            spriteRenderer.enabled = true;
            _collider.enabled = true;
            transform.position = onScreenSpawns[Random.Range(0, onScreenSpawns.Length)].transform.position;
            yield return new WaitForSeconds(Random.Range(2f, (float)currentEmotion) * 2);
            while (player.GetComponent<Sanity>().withinRangeOfEnemy_)
            {
                yield return null;
            }
            Disappear();
            //StartCoroutine(AppearOnScreenTimer());
        }*/

        /*private IEnumerator AppearOnScreenTimer()
        {
            recentlyScared = false;
            yield return new WaitForSeconds(Random.Range(7f, 12f) - (int)currentEmotion_);
            recentlyScared = true;
        }*/

        private IEnumerator AppearOffScreen()
        {
            GetComponent<AudioSource>().Play();
            Debug.Log("Appeared Off Screen");
            spriteRenderer.enabled = true;
            _collider.enabled = true;
            float random = Random.Range(0, 100);
            if(random <= 100 && random > 10)
            {
                transform.position = offScreenSpawns[Random.Range(0, 7)].transform.position;
            }
            else
            {
                transform.position = offScreenSpawns[Random.Range(7, offScreenSpawns.Length)].transform.position;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, 1);
            yield return null;
            animator.enabled = true;
            float timer = Random.Range(2f, (float)currentEmotion);
            while(timer > 0)
            {
                while (!player.GetComponent<Sanity>().withinRangeOfEnemy_ && timer > 0)
                {
                    if(!player.GetComponent<Sanity>().withinRangeOfEnemy_)
                    {
                        timer -= 0.1f;
                    }
                    yield return new WaitForSeconds(0.1f);
                }
                yield return null;
            }
            animator.SetTrigger("Reset");
            yield return null;
            spriteRenderer.enabled = false;
            _collider.enabled = false;
            animator.enabled = false;
            StartCoroutine(AppearOffScreenTimer());
        }

        private IEnumerator AppearOffScreenTimer()
        {
            recentlyScared = true;
            yield return new WaitForSeconds(Random.Range(7f, 12f) - (int)currentEmotion_);
            recentlyScared = false;
        }

        private void Scare()
        {
            Debug.Log("boo");
            StartCoroutine(ScareTimer());
        }

        private IEnumerator ScareTimer()
        {
            recentlyScared = false;
            yield return new WaitForSeconds(Random.Range(7f,12f) - (int)currentEmotion_);
            recentlyScared = true;
        }
    }
}