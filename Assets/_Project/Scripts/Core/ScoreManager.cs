using System;
using UnityEngine.SceneManagement;

namespace Gisha.BallGame.Core
{
    public class ScoreManager : IScoreManager
    {
        public int CurrentScore { get; private set; }
        public event Action<int> ScoreChanged;

        public ScoreManager()
        {
            SetScore(0);

            SceneManager.sceneLoaded += OnSceneLoaded;
            EventManager.StartListening(Constants.EVENT_OBSTACLE_DIED, objects => AddScore(100));
            EventManager.StartListening(Constants.EVENT_WIN, objects => AddScore(500));
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            SetScore(0);
        }

        public void Dispose()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            EventManager.StopListening(Constants.EVENT_OBSTACLE_DIED, objects => AddScore(100));
            EventManager.StopListening(Constants.EVENT_WIN, objects => AddScore(500));
        }

        public void AddScore(int addScore)
        {
            SetScore(CurrentScore + addScore);
        }

        private void SetScore(int score)
        {
            CurrentScore = score;
            ScoreChanged?.Invoke(CurrentScore);
        }
    }
}