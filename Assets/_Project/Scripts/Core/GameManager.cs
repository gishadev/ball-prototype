using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.BallGame.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public IWorldTouchController WorldTouchController { get; private set; }

        private bool _isWin, _isLose;

        private void Awake()
        {
            Instance = this;

            WorldTouchController = new WorldTouchController();

            EventManager.StartListening(Constants.EVENT_NEAR_TROPHY, Win);
            EventManager.StartListening(Constants.EVENT_MASS_ZERO, Lose);
        }

        private void OnDisable()
        {
            WorldTouchController.Dispose();

            EventManager.StopListening(Constants.EVENT_NEAR_TROPHY, Win);
            EventManager.StopListening(Constants.EVENT_MASS_ZERO, Lose);
        }

        private void Update()
        {
            WorldTouchController.Tick();
        }

        private void Win(Dictionary<string, object> dictionary)
        {
            if (_isWin || _isLose)
                return;

            Debug.Log("You win!");
            EventManager.TriggerEvent(Constants.EVENT_WIN, null);
            _isLose = false;
            _isWin = true;
        }

        private void Lose(Dictionary<string, object> obj)
        {
            if (_isWin || _isLose)
                return;

            Debug.Log("You lose!");
            EventManager.TriggerEvent(Constants.EVENT_LOSE, null);
            _isWin = false;
            _isLose = true;
        }

        public void Restart()
        {
            SceneManager.LoadScene(0);
            _isLose = false;
            _isWin = false;
        }
    }
}