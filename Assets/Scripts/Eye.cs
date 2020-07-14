using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eye : MonoBehaviour
{
    public KeyCode closeEye;

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
        if (Input.GetKeyDown(closeEye))
        {
            eyeImage.DOKill();
            eyeImage.DOFade(255f, 0.15f);
        }
        else if(Input.GetKeyUp(closeEye))
        {
            eyeImage.DOKill();
            eyeImage.DOFade(0f, 0.15f);
        }
    }
}
