using Platformer.Gameplay;
using UnityEngine;
using System.Collections;
using static Platformer.Core.Simulation;
using DG.Tweening;
using TMPro;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This class contains the data required for implementing token collection mechanics.
    /// It does not perform animation of the token, this is handled in a batch by the 
    /// TokenController in the scene.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Artifact : MonoBehaviour
    {
        public AudioClip artifactCollectAudio;
        [Tooltip("If true, animation will start at a random position in the sequence.")]
        public bool randomAnimationStartTime = false;
        [Tooltip("List of frames that make up the animation.")]
        public Sprite[] idleAnimation, investigateAnimation;

        internal Sprite[] sprites = new Sprite[0];

        internal SpriteRenderer _renderer;

        //unique index which is assigned by the TokenController in a scene.
        internal int artifactIndex = -1;
        //active frame in animation, updated by the controller.
        internal int frame = 0;
        internal bool investigated = false;

        [SerializeField] public KeyCode investigateKey = KeyCode.None;
        [SerializeField] private GameObject investigateUI = null;
        [SerializeField] private TextMeshProUGUI investigateText = null;
        public bool playerNear = false;

        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            if (randomAnimationStartTime)
                frame = Random.Range(0, sprites.Length);
            sprites = idleAnimation;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            //only exectue OnPlayerEnter if the player collides with this token.
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                playerNear = true;
                StartCoroutine(OnPlayerEnter(player));
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                playerNear = false;
                StartCoroutine(OnPlayerExit());
            }
        }

        IEnumerator OnPlayerEnter(PlayerController player)
        {
            investigateUI.SetActive(true);
            while (playerNear)
            {
                if(Input.GetKeyDown(investigateKey))
                {
                    if (!investigated)
                    {
                        frame = 0;
                        sprites = investigateAnimation;
                        investigated = true;
                        ArtifactController.instance.artifactsRemaining_--;
                        AudioSource.PlayClipAtPoint(artifactCollectAudio, transform.position);
                        investigateText.text = gameObject.name;
                    }
                }
                yield return null;
            }
        }

        IEnumerator OnPlayerExit()
        {
            //investigateUI.transform.DOLocalMove(Vector3.down / 2, 0.1f);
            yield return new WaitForSeconds(0.1f);
            investigateUI.SetActive(false);
            //investigateUI.transform.localPosition += Vector3.up;
        }
    }
}