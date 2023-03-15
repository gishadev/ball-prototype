using System;
using Gisha.BallGame.Core;
using UnityEngine;

namespace Gisha.BallGame.Ball
{
    [RequireComponent(typeof(PlayerBall))]
    public class BallShooting : MonoBehaviour
    {
        private IWorldTouchController _worldTouchController;
        private PlayerBall _playerBall;
        private GameDataSO _gameData;
        private MassAccumulator _massAccumulator;

        public MassAccumulator MassAccumulator => _massAccumulator;

        private void Awake()
        {
            _playerBall = GetComponent<PlayerBall>();
            _gameData = ResourceGetter.GetGameData();
            _massAccumulator = new MassAccumulator(_gameData.MassAccumulationSpeed, _playerBall,
                _gameData.MinAccumulationMass);
        }

        private void Start()
        {
            _worldTouchController = GameManager.Instance.WorldTouchController;
            _worldTouchController.WorldTouchDown += OnWorldWorldTouchDown;
            _worldTouchController.WorldTouchUp += OnWorldWorldTouchUp;
        }

        private void OnDisable()
        {
            _worldTouchController.WorldTouchDown -= OnWorldWorldTouchDown;
            _worldTouchController.WorldTouchUp -= OnWorldWorldTouchUp;
        }

        private void Update()
        {
            if (_worldTouchController.IsFingerDown)
                MassAccumulator.AccumulateMassInTick();
        }

        private void OnWorldWorldTouchDown(Vector3 pos)
        {
            MassAccumulator.ResetMass();
        }

        private void OnWorldWorldTouchUp(Vector3 pos)
        {
            var projMass = CalculateProjectileMass(MassAccumulator.Mass);
            Shoot(pos, projMass);

            _playerBall.AddMass(-MassAccumulator.Mass);
            
            EventManager.TriggerEvent(Constants.EVENT_SHOOT, null);
        }

        private void Shoot(Vector3 touchPos, float shootMass)
        {
            var dir = (touchPos - transform.position).normalized;
            dir.y = 0f;
            var rotation = Quaternion.LookRotation(dir);

            var proj = Instantiate(_gameData.ProjectilePrefab, transform.position, rotation);
            proj.transform.localScale = Vector3.one * shootMass;
        }

        private float CalculateProjectileMass(float entryMass)
        {
            var maxedMass = entryMass + _gameData.ProjectileAdditionalMass;

            if (_playerBall.CurrentMass > maxedMass)
                return maxedMass;

            return entryMass;
        }
    }
}