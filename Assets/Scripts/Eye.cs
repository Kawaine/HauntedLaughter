using DG.Tweening;
using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eye : MonoBehaviour
{
    public PlayerController player;
    public KeyCode closeEye;
    public CameraFilterPack_Atmosphere_Rain rain;
    public AudioSource breathein;
    public AudioSource heartbeat;
    public AudioSource breatheout;

    public float eyeAlphaChangePerTick = 5f;

    private float closedEyePercentage = 0;
    public float closedEyePercentage_
    {
        get
        {
            return closedEyePercentage;
        }
        set
        {
            if(value > 100 || value < 0)
            {
                return;
            }
            closedEyePercentage = value;
        }
    }

    public SpriteRenderer eyeImage;

    // Update is called once per frame
    void Update()
    {
        if(player.controlEnabled)
        {
            closedEyePercentage = eyeImage.color.a;
            if (Input.GetKeyDown(closeEye))
            {
                breathein.Play();
                heartbeat.Play();
                rain.enabled = false;
                eyeImage.DOKill();
                eyeImage.DOFade(255f, 0.15f);
            }
            if (Input.GetKeyUp(closeEye))
            {
                heartbeat.Stop();
                breatheout.Play();
                rain.enabled = true;
                eyeImage.DOKill();
                eyeImage.DOFade(0f, 0.15f);
            }
        }
    }
}
