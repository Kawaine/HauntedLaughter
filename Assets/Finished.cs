using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class Finished : MonoBehaviour
{
    private Artifact artifact = null;
    private EnemyController enemy = null;
    private Sanity sanity = null;

    private void Start()
    {
        artifact = GetComponent<Artifact>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        //only exectue OnPlayerEnter if the player collides with this token.
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            StartCoroutine(OnPlayerEnter(player));
        }
    }

    IEnumerator OnPlayerEnter(PlayerController player)
    {
        while (artifact.playerNear)
        {
            if (Input.GetKeyDown(artifact.investigateKey))
            {
                enemy.currentEmotion_ = EnemyController.Emotions.Asleep;
                sanity.finished = true;
            }
            yield return null;
        }
    }
}
