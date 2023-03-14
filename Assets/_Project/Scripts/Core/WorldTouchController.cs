using System;
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
        }

        public void Dispose()
        {
            EnhancedTouch.Touch.onFingerDown -= OnFingerDown;
            EnhancedTouch.Touch.onFingerUp -= OnFingerUp;
        }

        public void Tick()
        {
            if (!IsFingerDown)
                return;

            SetFingerWorldPositon();
        }

        private void SetFingerWorldPositon()
        {
            var screenPosition = EnhancedTouch.Touch.activeTouches[0].screenPosition;
            var ray = Camera.main.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out var hitInfo, 100f))
                FingerWorldPosition = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
        }

        private void OnFingerDown(EnhancedTouch.Finger finger)
        {
            IsFingerDown = true;
            SetFingerWorldPositon();
            WorldTouchDown?.Invoke(FingerWorldPosition);
        }

        private void OnFingerUp(EnhancedTouch.Finger finger)
        {
            IsFingerDown = false;
            WorldTouchUp?.Invoke(FingerWorldPosition);
        }
    }
}