using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Gisha.BallGame.World
{
    public abstract class Obstacle : MonoBehaviour, IObstacle
    {
        public bool IsDied { get; }

        private MeshRenderer _mr;

        private void Awake()
        {
            _mr = GetComponent<MeshRenderer>();
        }

        public abstract void Die();

        public async void Infect(Material infectMaterial)
        {
            _mr.material = infectMaterial;
            await Task.Delay(200);
            Die();
        }
    }
}