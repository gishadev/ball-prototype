using Gisha.BallGame.Core;
using UnityEngine;

namespace Gisha.BallGame.World
{
    public class StationaryObstacle : MonoBehaviour, IObstacle
    {
        public void Die()
        {
            EventManager.TriggerEvent(Constants.EVENT_OBSTACLE_DIED, null);
            Destroy(gameObject);
        }
    }
}