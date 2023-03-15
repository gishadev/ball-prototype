using System.Collections.Generic;
using Gisha.BallGame.Core;
using Gisha.BallGame.World;
using UnityEngine;

namespace Gisha.BallGame.Ball
{
    public class Projectile : MonoBehaviour
    {
        private GameDataSO _gameData;

        private float ProjectileRadius => transform.localScale.x / 2f;
        private float ExplosionRadius => ProjectileRadius * _gameData.ExplosionRadiusMultiplier;
        
        private void Awake()
        {
            _gameData = ResourceGetter.GetGameData();
        }

        private void Update()
        {
            MoveForward();
            if (IsRaycastedObstacle())
            {
                var obstacles = GetObstaclesAround();
                foreach (var obstacle in obstacles)
                    obstacle.Die();

                Destroy(gameObject);
            }
        }

        private void MoveForward()
        {
            transform.Translate(transform.forward * (_gameData.ProjectileFlySpeed * Time.deltaTime), Space.World);
        }

        private bool IsRaycastedObstacle()
        {
            var colls = Physics.OverlapSphere(transform.position, ProjectileRadius);
            foreach (var coll in colls)
                if (coll.TryGetComponent(out IObstacle obstacle))
                    return true;

            return false;
        }

        private List<IObstacle> GetObstaclesAround()
        {
            List<IObstacle> result = new List<IObstacle>();
            var colls = Physics.OverlapSphere(transform.position, ExplosionRadius);
            foreach (var coll in colls)
                if (coll.TryGetComponent(out IObstacle obstacle))
                    result.Add(obstacle);

            return result;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
        }
    }
}