using Gisha.BallGame.Core;
using UnityEngine;

namespace Gisha.BallGame.World
{
    public class StationaryObstacle : MonoBehaviour, IObstacle
    {
        public bool IsDied { get; private set; }

        public void Die()
        {
            if (IsDied)
                return;
            
            Destroy(gameObject);
            EventManager.TriggerEvent(Constants.EVENT_OBSTACLE_DIED, null);
            
            IsDied = true;
        }
    }
}