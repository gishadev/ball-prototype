using System;
using System.Collections.Generic;
using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

namespace Gisha.BallGame.Core
{
    public class WorldTouchController : IWorldTouchController
    {
        public Vector3 FingerWorldPosition { get; private set; }
        public bool IsFingerDown { get; private set; }

        public event Action<Vector3> WorldTouchDown;
        public event Action<Vector3> WorldTouchUp;

        private bool _isEnabled = true;

        public WorldTouchController()
        {
            Init();
        }

        public void Init()
        {
            EnhancedTouch.EnhancedTouchSupport.Enable();
            EnhancedTouch.TouchSimulation.Enable();

            EnhancedTouch.Touch.onFingerDown += OnFingerDown;
            EnhancedTouch.Touch.onFingerUp += OnFingerUp;

            EventManager.StartListening(Constants.EVENT_PATH_CLEARED, OnPathCleared);
        }

        public void Dispose()
        {
            EnhancedTouch.Touch.onFingerDown -= OnFingerDown;
            EnhancedTouch.Touch.onFingerUp -= OnFingerUp;

            EventManager.StopListening(Constants.EVENT_PATH_CLEARED, OnPathCleared);
        }

        private void OnPathCleared(Dictionary<string, object> obj)
        {
            _isEnabled = false;
            IsFingerDown = false;
        }

        public void Tick()
        {
            if (!IsFingerDown || !_isEnabled)
                return;

            SetFingerWorldPositon();
        }

        private void SetFingerWorldPositon()
        {
            if (EnhancedTouch.Touch.activeTouches.Count == 0)
                return;

            var screenPosition = EnhancedTouch.Touch.activeTouches[0].screenPosition;
            var ray = Camera.main.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out var hitInfo, 100f))
                FingerWorldPosition = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
        }

        private void OnFingerDown(EnhancedTouch.Finger finger)
        {
            if (!_isEnabled)
                return;

            IsFingerDown = true;
            SetFingerWorldPositon();
            WorldTouchDown?.Invoke(FingerWorldPosition);
        }

        private void OnFingerUp(EnhancedTouch.Finger finger)
        {
            if (!_isEnabled)
                return;

            IsFingerDown = false;
            WorldTouchUp?.Invoke(FingerWorldPosition);
        }
    }
}