using System;
using Gisha.BallGame.Core;
using UnityEngine;

namespace Gisha.BallGame.Ball
{
    [RequireComponent(typeof(PlayerBall))]
    public class BallShooting : MonoBehaviour
    {
        private IWorldTouchController _worldTouchController;
        private PlayerBall _playerBall;
        private GameDataSO _gameData;

        private void Awake()
        {
            _playerBall = GetComponent<PlayerBall>();
            _gameData = ResourceGetter.GetGameData();
        }

        private void Start()
        {
            _worldTouchController = GameManager.Instance.WorldTouchController;
            _worldTouchController.WorldTouchDown += OnWorldWorldTouchDown;
            _worldTouchController.WorldTouchUp += OnWorldWorldTouchUp;
        }

        private void OnDisable()
        {
            _worldTouchController.WorldTouchDown -= OnWorldWorldTouchDown;
            _worldTouchController.WorldTouchUp -= OnWorldWorldTouchUp;
        }

        private void OnWorldWorldTouchDown(Vector3 pos)
        {
        }

        private void OnWorldWorldTouchUp(Vector3 pos)
        {
            var dir = (pos - transform.position).normalized;
            dir.y = 0f;
            var rotation = Quaternion.LookRotation(dir);

            Instantiate(_gameData.ProjectilePrefab, transform.position, rotation);
        }
    }
}