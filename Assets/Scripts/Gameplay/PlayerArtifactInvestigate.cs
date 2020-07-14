using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when a player collides with a token.
    /// </summary>
    /// <typeparam name="PlayerCollision"></typeparam>
    public class PlayerArtifactInvestigate : Simulation.Event<PlayerArtifactInvestigate>
    {
        public Artifact artifact;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            AudioSource.PlayClipAtPoint(artifact.artifactCollectAudio, artifact.transform.position);
            if(!artifact.investigated)
            {
                artifact.investigated = true;
                ArtifactController.instance.artifactsRemaining_--;

            }
        }
    }
}