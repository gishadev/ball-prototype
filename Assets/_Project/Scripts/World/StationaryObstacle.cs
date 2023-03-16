using Gisha.BallGame.Core;

namespace Gisha.BallGame.World
{
    public class StationaryObstacle : Obstacle
    {
        public bool IsDied { get; private set; }
        
        public override void Die()
        {
            if (IsDied)
                return;

            Destroy(gameObject);
            EventManager.TriggerEvent(Constants.EVENT_OBSTACLE_DIED, null);

            IsDied = true;
        }
    }
}