using System.Collections.Generic;
using Gisha.BallGame.Ball;
using Gisha.BallGame.Core;
using UnityEngine;

namespace Gisha.BallGame.World
{
    public class PathResizer : MonoBehaviour
    {
        [SerializeField] private PlayerBall playerBall;
        [SerializeField] private LineRenderer pathLine;

        private void OnEnable()
        {
            EventManager.StartListening(Constants.EVENT_SHOOT, ResizePath);
        }

        private void OnDisable()
        {
            EventManager.StopListening(Constants.EVENT_SHOOT, ResizePath);
        }

        private void ResizePath(Dictionary<string, object> dictionary)
        {
            pathLine.startWidth = playerBall.CurrentMass;
            pathLine.endWidth = playerBall.CurrentMass;
        }
    }
}