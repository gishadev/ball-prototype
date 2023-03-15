using System;
using System.Collections.Generic;
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

            EventManager.StartListening(Constants.EVENT_PATH_CLEARED, Win);
        }

        private void OnDisable()
        {
            WorldTouchController.Dispose();

            EventManager.StopListening(Constants.EVENT_PATH_CLEARED, Win);
        }

        private void Update()
        {
            WorldTouchController.Tick();
        }

        private void Win(Dictionary<string, object> dictionary)
        {
            Debug.Log("You win!");
        }
    }
}