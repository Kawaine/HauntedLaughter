using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class ArtifactController : MonoBehaviour
    {
        public static ArtifactController instance = null;

        [SerializeField] private Artifact[] artifacts = null;
        [SerializeField] private EnemyController enemy = null;

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
                enemy.currentEmotion_ += 1;
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
