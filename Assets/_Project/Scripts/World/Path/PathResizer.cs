using System.Collections;
using System.Collections.Generic;
using Gisha.BallGame.Ball;
using Gisha.BallGame.Core;
using UnityEngine;

namespace Gisha.BallGame.World
{
    public class PathResizer : MonoBehaviour
    {
        [SerializeField] private LineRenderer pathLine;
        [SerializeField] private float widthChangingSpeed = 2f;

        private PlayerBall PlayerBall => GameManager.Instance.PlayerBall;
        private float _width;

        private void Start()
        {
            _width = PlayerBall.CurrentMass;
            ApplyWidth();
        }

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
            StartCoroutine(ResizingRoutine());
        }

        private IEnumerator ResizingRoutine()
        {
            while (_width > PlayerBall.CurrentMass)
            {
                _width = Mathf.Lerp(_width, PlayerBall.CurrentMass, widthChangingSpeed * Time.deltaTime);
                ApplyWidth();
                yield return null;
            }

            ApplyWidth();
        }

        private void ApplyWidth()
        {
            pathLine.startWidth = _width;
            pathLine.endWidth = _width;
        }
    }
}