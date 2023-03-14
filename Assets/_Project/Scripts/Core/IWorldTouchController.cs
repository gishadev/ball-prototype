using System;
using UnityEngine;

namespace Gisha.BallGame.Core
{
    public interface IWorldTouchController
    {
        Vector3 FingerWorldPosition { get; }
        bool IsFingerDown { get; }
        event Action<Vector3> WorldTouchDown;
        event Action<Vector3> WorldTouchUp;
        void Init();
        void Dispose();
        void Tick();
    }
}