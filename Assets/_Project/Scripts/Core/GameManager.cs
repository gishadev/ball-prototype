using System;
using UnityEngine;

namespace Gisha.BallGame.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public IWorldTouchController WorldTouchController { get; private set; }

        private void Awake()
        {
            Instance = this;

            WorldTouchController = new WorldTouchController();
        }

        private void OnDisable()
        {
            WorldTouchController.Dispose();
        }

        private void Update()
        {
            WorldTouchController.Tick();
        }
    }
}