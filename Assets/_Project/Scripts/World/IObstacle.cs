using UnityEngine;

namespace Gisha.BallGame.World
{
    public interface IObstacle
    {
        Transform transform { get; }
        GameObject gameObject { get; }
    }
}