using UnityEngine;

namespace Gisha.BallGame.Ball
{
    public class MassAccumulator
    {
        private readonly float _speed;
        private readonly PlayerBall _playerBall;
        private readonly float _minMass;
        public float Mass { get; private set; }

        public MassAccumulator(float speed, PlayerBall playerBall, float minMass)
        {
            _speed = speed;
            _playerBall = playerBall;
            _minMass = minMass;

            ResetMass();
        }

        public void AccumulateMassInTick()
        {
            if (Mass < _playerBall.CurrentMass)
                Mass += _speed * Time.deltaTime;
            else
                Mass = _playerBall.CurrentMass;
        }

        public void ResetMass()
        {
            if (_playerBall.CurrentMass < _minMass)
                Mass = _playerBall.CurrentMass;

            Mass = _minMass;
        }
    }
}