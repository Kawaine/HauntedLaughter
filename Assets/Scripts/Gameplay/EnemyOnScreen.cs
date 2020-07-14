using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when a player collides with a token.
    /// </summary>
    /// <typeparam name="PlayerCollision"></typeparam>
    public class EnemyOnScreen : Simulation.Event<EnemyOnScreen>
    {
        public Sanity sanity;
        public EnemyController enemy;

        public override void Execute()
        {
            sanity.SpottedEnemy();
        }
    }
}