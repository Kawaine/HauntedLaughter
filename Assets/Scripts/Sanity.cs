using Platformer.Gameplay;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Platformer.Core.Simulation;
using SuperShaders.Screen;
using UnityEngine.UI;
using DG.Tweening;

namespace Platformer.Mechanics
{
    public class Sanity : SerializedMonoBehaviour
    {
        [SerializeField] private SpriteRenderer Clown = null;
        [SerializeField] private Material BlurMaterial = null;
        [SerializeField] private Color startingClownColor;
        [SerializeField] private Color endingClownColor;
        [SerializeField] private SpriteRenderer Background = null;
        [SerializeField] private Color startingBackgroundColor;
        [SerializeField] private Color endingBackgroundColor;
        [SerializeField] private AudioSource glitchFx = null;
        [SerializeField] private AudioSource rainFX = null;
        [SerializeField] private CameraFilterPack_Atmosphere_Rain rain = null;
        [SerializeField, MinMaxSlider(0, 1)] private Vector2 BlurAmount = Vector2.zero;

        [SerializeField] private ScreenScanlineEffect screenScanline = null;
        [SerializeField] private Color startingScanlineColor;
        [SerializeField] private Color endingScanlineColor;

        [ReadOnly] public bool finished = false;
        private bool withinRangeOfEnemy = false;
        public bool withinRangeOfEnemy_
        {
            get 
            {
                return withinRangeOfEnemy;
            }
            set
            {
                if(!finished)
                {
                    withinRangeOfEnemy = value;
                }
                else
                {
                    withinRangeOfEnemy = false;
                }
            }
        }

        public bool recentlyReducedSanity = false;



        private float baseSanity = 100;
        [ShowInInspector, LabelText("Base Sanity Level")]
        public float baseSanity_
        {
            get
            {
                return baseSanity;
            }
            set
            {
                baseSanity = value;
            }
        }

        private float currentSanity = 100;
        public float currentSanity_
        {
            get
            {
                return currentSanity;
            }
            set
            {
                currentSanity = value;
                //screenScanline.material.
                //change clown alpha based on current sanity
                Clown.color = Color.Lerp(endingClownColor, startingClownColor, currentSanity / 100);
                Background.color = Color.Lerp(endingBackgroundColor, startingBackgroundColor, currentSanity / 100);
                screenScanline.currentColor = Color.Lerp(endingScanlineColor, startingScanlineColor, currentSanity / 100);
                glitchFx.volume = Mathf.Lerp(1, 0.3f, currentSanity / 100);
                rainFX.volume = Mathf.Lerp(0.2f, 0, currentSanity / 100);
                rain.Fade = Mathf.Lerp(0, 0.5f, currentSanity / 100);
                if (!finished)
                {
                    if (currentSanity <= 0)
                    {
                        StartCoroutine(Restart());
                    }
                }
            }
        }

        private IEnumerator Restart()
        {
            gameObject.GetComponent<PlayerController>().controlEnabled = false;
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(0);
        }

        [SerializeField] private EnemyController enemy = null;
        [SerializeField] private ScreenDistortEffect screenDistort = null;
        [SerializeField] private ScreenNegativeEffect screenNegative = null;
        [SerializeField] private Eye eye = null;

        private IEnumerator recoverSanity = null;
        private bool recoveringSanity = false;


        private void Start()
        {
            StartCoroutine(CalculateSanityEnum());
            recoverSanity = RecoverSanity();
            StartCoroutine(recoverSanity);
        }

        public void SpottedEnemy()
        {
            withinRangeOfEnemy = true;
        }

        private IEnumerator CalculateSanityEnum()
        {
            while(true)
            {
                while (withinRangeOfEnemy_)
                {
                    float damage = 0;
                    if(eye.closedEyePercentage_ > 1)
                    {
                        damage = 0;
                    }
                    else if (enemy.currentEmotion_ == EnemyController.Emotions.Quiet)
                    {
                        damage += 5;
                    }
                    else if (enemy.currentEmotion_ == EnemyController.Emotions.Curious)
                    {
                        damage += 12;
                    }
                    else if (enemy.currentEmotion_ == EnemyController.Emotions.Interested)
                    {
                        damage += 33.4f;
                    }
                    else if (enemy.currentEmotion_ == EnemyController.Emotions.Angry)
                    {
                        damage += 49f;
                    }
                    else if (enemy.currentEmotion_ == EnemyController.Emotions.Enraged)
                    {
                        damage += 99f;
                    }
                    damage /= 10f;
                    currentSanity_ -= damage;
                    recentlyReducedSanity = true;
                    yield return new WaitForSeconds(0.1f);
                }
                yield return null;
            }
        }

        private IEnumerator RecoverSanity()
        {
            while(true)
            {
                while (currentSanity_ < 100)
                {
                    if (!withinRangeOfEnemy_)
                    {
                        if (recentlyReducedSanity)
                        {
                            yield return new WaitForSeconds(2f + (float) enemy.currentEmotion_);
                            recentlyReducedSanity = false;
                        }
                        if(!withinRangeOfEnemy_)
                        {
                            if (currentSanity_ <= 100)
                            {
                                currentSanity_ += 30f / (float) enemy.currentEmotion_;
                            }
                            if (currentSanity_ > 100)
                            {
                                currentSanity_ = 100;
                            }
                            yield return new WaitForSeconds(0.5f);
                        }
                    }
                    yield return null;
                }
                yield return null;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Clown")
            {
                currentSanity_ = 0;
            }
        }
    }
}

