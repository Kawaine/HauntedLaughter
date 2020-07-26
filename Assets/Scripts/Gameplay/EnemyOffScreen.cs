using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when a player collides with a token.
    /// </summary>
    /// <typeparam name="PlayerCollision"></typeparam>
    public class EnemyOffScreen : Simulation.Event<EnemyOffScreen>
    {
        public Sanity sanity;
        public EnemyController enemy;

        public override void Execute()
        {
            sanity.withinRangeOfEnemy_ = false;
        }
    }
}