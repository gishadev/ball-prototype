using System;
using System.Collections;
using System.Collections.Generic;
using Gisha.BallGame.Core;
using Gisha.BallGame.World;
using UnityEngine;

namespace Gisha.BallGame.Ball
{
    public class BallMovement : MonoBehaviour
    {
        [SerializeField] private PathBuilder pathBuilder;

        private GameDataSO _gameData;
        private Animator _animator;

        private void Awake()
        {
            _gameData = ResourceGetter.GetGameData();
            _animator = GetComponent<Animator>();

            EventManager.StartListening(Constants.EVENT_PATH_CLEARED, OnPathCleared);
        }

        private void OnDisable()
        {
            EventManager.StopListening(Constants.EVENT_PATH_CLEARED, OnPathCleared);
        }

        private void OnPathCleared(Dictionary<string, object> obj)
        {
            StartCoroutine(MoveRoutine());
            _animator.SetBool("IsJumping", true);
        }

        private IEnumerator MoveRoutine()
        {
            var positions = EnquePositions();

            while (positions.Count > 0)
            {
                Vector3 target = positions.Peek();
                target.y = transform.position.y;

                if ((target - transform.position).magnitude < 0.1f)
                    positions.Dequeue();
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, target,
                        _gameData.BallMovementSpeed * Time.deltaTime);
                }

                yield return null;
            }
        }

        private Queue<Vector3> EnquePositions()
        {
            Queue<Vector3> positions = new Queue<Vector3>();
            for (var i = 1; i < pathBuilder.Points.Length; i++)
                positions.Enqueue(pathBuilder.Points[i].transform.position);

            return positions;
        }
    }
}