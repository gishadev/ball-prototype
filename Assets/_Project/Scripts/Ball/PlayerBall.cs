using Gisha.BallGame.Core;
using UnityEngine;

namespace Gisha.BallGame.Ball
{
    public class PlayerBall : MonoBehaviour
    {
        private GameDataSO _gameData;
        private float _currentMass;

        public float CurrentMass => _currentMass;

        private void Awake()
        {
            _gameData = ResourceGetter.GetGameData();
            _currentMass = transform.localScale.x;
        }

        public void AddMass(float value)
        {
            _currentMass = CurrentMass + value;
            ApplyMass();
        }

        public void ApplyMass()
        {
            transform.localScale = Vector3.one * CurrentMass;
        }
    }
}