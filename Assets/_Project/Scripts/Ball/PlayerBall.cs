using Gisha.BallGame.Core;
using UnityEngine;

namespace Gisha.BallGame.Ball
{
    public class PlayerBall : MonoBehaviour
    {
        private GameDataSO _gameData;
        private float _currentMass = 1f;

        private void Awake()
        {
            _gameData = ResourceGetter.GetGameData();
        }
    }
}