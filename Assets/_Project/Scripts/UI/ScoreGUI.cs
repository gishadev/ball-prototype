using Gisha.BallGame.Core;
using TMPro;
using UnityEngine;

namespace Gisha.BallGame.UI
{
    public class ScoreGUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        private ScoreManager ScoreManager => GameManager.Instance.ScoreManager;

        private void Start()
        {
            ScoreManager.ScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            ScoreManager.ScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}