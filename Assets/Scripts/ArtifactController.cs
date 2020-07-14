using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class ArtifactController : MonoBehaviour
    {
        public static ArtifactController instance = null;

        [SerializeField] internal Artifact[] artifacts;

        private int artifactsRemaining = 6;
        public int artifactsRemaining_
        {
            get
            {
                return artifactsRemaining;
            }
            set
            {
                artifactsRemaining = value;
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            artifactsRemaining_ = artifacts.Length;
        }

        public int ArtifactsInvestigated()
        {
            return artifacts.Length - artifactsRemaining;
        }
    }
}
