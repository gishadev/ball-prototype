using System;
using Gisha.BallGame.Core;
using UnityEngine;

namespace Gisha.BallGame.Ball
{
    [RequireComponent(typeof(PlayerBall))]
    public class BallShooting : MonoBehaviour
    {
        private PlayerBall _playerBall;
        private IWorldTouchController _worldTouchController;
        
        private void Awake()
        {
            _playerBall = GetComponent<PlayerBall>();
        }

        private void Start()
        {
            _worldTouchController = GameManager.Instance.WorldTouchController;
            _worldTouchController.WorldTouchDown += OnWorldWorldTouchDown;
            _worldTouchController.WorldTouchUp += OnWorldWorldTouchUp;
        }

        private void OnDisable()
        {
            
        }

        private void OnWorldWorldTouchDown(Vector3 pos)
        {
            
        }

        private void OnWorldWorldTouchUp(Vector3 pos)
        {
            Debug.DrawLine(transform.position, _worldTouchController.FingerWorldPosition, Color.red, 0.5f);
        }
    }
}