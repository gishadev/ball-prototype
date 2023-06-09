﻿using Gisha.BallGame.Ball;
using Gisha.BallGame.Core;
using UnityEngine;

namespace Gisha.BallGame.World
{
    public class Trophy : MonoBehaviour
    {
        private PlayerBall PlayerBall => GameManager.Instance.PlayerBall;

        private GameDataSO _gameData;
        private bool _checkForPlayer;

        private void Awake()
        {
            _gameData = ResourceGetter.GetGameData();
            EventManager.StartListening(Constants.EVENT_PATH_CLEARED, objects => _checkForPlayer = true);
        }

        private void OnDisable()
        {
            EventManager.StopListening(Constants.EVENT_PATH_CLEARED, objects => _checkForPlayer = true);
        }

        private void Update()
        {
            if (!_checkForPlayer)
                return;

            if (Vector3.Distance(PlayerBall.transform.position, transform.position) < _gameData.TrophyWinRadius)
                CollectTrophy();
        }

        private void CollectTrophy()
        {
            EventManager.TriggerEvent(Constants.EVENT_NEAR_TROPHY, null);
            transform.GetChild(0).gameObject.SetActive(false);
            _checkForPlayer = false;
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _gameData.TrophyWinRadius);
        }
    }
}