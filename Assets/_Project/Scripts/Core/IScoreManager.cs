using System;

namespace Gisha.BallGame.Core
{
    public interface IScoreManager
    {
        int CurrentScore { get; }
        void AddScore(int addScore);
        event Action<int> ScoreChanged;

        void Dispose();
    }
}