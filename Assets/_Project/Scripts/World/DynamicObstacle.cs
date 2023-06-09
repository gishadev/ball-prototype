﻿using Gisha.BallGame.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gisha.BallGame.World
{
    public class DynamicObstacle : Obstacle
    {
        [SerializeField] private Vector3 moveDir;
        [SerializeField] private float moveSpeed = 2f;

        public bool IsDied { get; private set; }

        private Vector3 _fPoint, _sPoint;
        private Vector3 _currentPoint;
        
        private void Start()
        {
            moveDir.y = 0f;
            _fPoint = transform.position + moveDir;
            _sPoint = transform.position - moveDir;

            _currentPoint = Random.Range(0f, 1f) < 0.5f ? _fPoint : _sPoint;
        }

        private void Update()
        {
            if ((transform.position - _currentPoint).magnitude < 0.1f)
                _currentPoint = (_currentPoint - _fPoint).magnitude < 0.05f ? _sPoint : _fPoint;

            transform.position = Vector3.MoveTowards(transform.position, _currentPoint, moveSpeed * Time.deltaTime);
        }


        public override void Die()
        {
            if (IsDied)
                return;

            EventManager.TriggerEvent(Constants.EVENT_OBSTACLE_DIED, null);
            Destroy(gameObject);

            IsDied = true;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(transform.position, moveDir);
            Gizmos.DrawRay(transform.position, -moveDir);
        }
    }
}