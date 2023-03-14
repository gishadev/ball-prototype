using System;
using Gisha.BallGame.Core;
using Gisha.BallGame.World;
using UnityEngine;

namespace Gisha.BallGame.Ball
{
    public class Projectile : MonoBehaviour
    {
        private GameDataSO _gameData;

        private void Awake()
        {
            _gameData = ResourceGetter.GetGameData();
        }

        private void Update()
        {
            MoveForward();
            if (IsRaycastedObstacle(out var obstacle))
            {
                Destroy(obstacle.gameObject);
                Destroy(gameObject);
            }
        }

        private void MoveForward()
        {
            transform.Translate(transform.forward * (_gameData.ProjectileFlySpeed * Time.deltaTime), Space.World);
        }

        private bool IsRaycastedObstacle(out IObstacle obstacle)
        {
            var colls = Physics.OverlapSphere(transform.position, transform.localScale.x);
            foreach (var coll in colls)
            {
                if (coll.TryGetComponent(out obstacle))
                    return true;
            }

            obstacle = null;
            return false;
        }
    }
}