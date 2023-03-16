using UnityEngine;

namespace Gisha.BallGame.World
{
    public interface IObstacle
    {
        Transform transform { get; }
        GameObject gameObject { get; }

        bool IsDied { get; }
        void Die();
        void Infect(Material infectMaterial);
    }
}