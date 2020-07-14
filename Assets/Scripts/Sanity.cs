using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Platformer.Mechanics
{
    public class Sanity : SerializedMonoBehaviour
    {
        public bool withinRangeOfEnemy = false;

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
            }
        }

        [SerializeField] private GameObject Clown = null;
        [SerializeField] private Eye eye = null;

        private float distanceFromClown = 1000;
        public float distanceFromClown_
        {
            get
            {
                return distanceFromClown;
            }
            set
            {
                distanceFromClown = value;
            }
        }

        private void Update()
        {
            distanceFromClown_ = Mathf.Abs(Vector3.Distance(transform.position, Clown.transform.position));
        }

        public void SpottedEnemy()
        {
            if(currentSanity_ == 100)
            {
                StartCoroutine(CalculateSanityEnum());
            }
            withinRangeOfEnemy = true;
        }

        private IEnumerator CalculateSanityEnum()
        {
            while(withinRangeOfEnemy && currentSanity_ < 100)
            {
                while (withinRangeOfEnemy)
                {
                    float damage = ArtifactController.instance.ArtifactsInvestigated();
                    currentSanity_ -= damage;
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}

