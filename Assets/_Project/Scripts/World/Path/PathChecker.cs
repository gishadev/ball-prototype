using System.Collections.Generic;
using System.Threading.Tasks;
using Gisha.BallGame.Core;
using UnityEngine;

namespace Gisha.BallGame.World
{
    public class PathChecker : MonoBehaviour
    {
        [SerializeField] private LineRenderer pathLine;

        private void OnEnable()
        {
            EventManager.StartListening(Constants.EVENT_OBSTACLE_DIED, CheckPath);
            EventManager.StartListening(Constants.EVENT_SHOOT, CheckPath);
        }

        private void OnDisable()
        {
            EventManager.StopListening(Constants.EVENT_OBSTACLE_DIED, CheckPath);
            EventManager.StopListening(Constants.EVENT_SHOOT, CheckPath);
        }

        private async void CheckPath(Dictionary<string, object> obj)
        {
            await Task.Delay(500);
            for (int i = 0; i < pathLine.positionCount - 1; i++)
            {
                var fPos = pathLine.GetPosition(i);
                var sPos = pathLine.GetPosition(i + 1);

                var ray = new Ray(fPos, sPos - fPos);
                float radius = (pathLine.startWidth + pathLine.endWidth) / 4f;
                var hits = Physics.SphereCastAll(ray, radius, (sPos - fPos).magnitude);

                foreach (var hitInfo in hits)
                {
                    if (hitInfo.collider.TryGetComponent(out IObstacle obstacle))
                        return;
                }

                EventManager.TriggerEvent(Constants.EVENT_PATH_CLEARED, null);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;

            for (int i = 0; i < pathLine.positionCount - 1; i++)
            {
                var fPos = pathLine.GetPosition(i);
                var sPos = pathLine.GetPosition(i + 1);

                float radius = (pathLine.startWidth + pathLine.endWidth) / 4f;
                Gizmos.DrawLine(fPos, sPos);
                Gizmos.DrawWireSphere(fPos, radius);
                Gizmos.DrawWireSphere(sPos, radius);
            }
        }
    }
}