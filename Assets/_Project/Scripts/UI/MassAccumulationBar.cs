using Gisha.BallGame.Ball;
using Gisha.BallGame.Core;
using UnityEngine;

namespace Gisha.BallGame.UI
{
    public class MassAccumulationBar : MonoBehaviour
    {
        [SerializeField] private Transform accumulationBarTrans;

        private BallShooting BallShooting => GameManager.Instance.PlayerBall.BallShooting;
        private MassAccumulator _massAccumulator;
        private IWorldTouchController _touchController;

        private void Start()
        {
            _massAccumulator = BallShooting.MassAccumulator;
            _touchController = GameManager.Instance.WorldTouchController;

            _touchController.WorldTouchUp += OnFingerUp;
        }

        private void OnDisable()
        {
            _touchController.WorldTouchUp -= OnFingerUp;
        }

        private void Update()
        {
            if (!_touchController.IsFingerDown)
                return;

            Rescale();
        }

        private void OnFingerUp(Vector3 pos)
        {
            Reset();
        }

        private void Rescale()
        {
            if (_massAccumulator.MaxMass == 0f)
                return;

            float xScale = _massAccumulator.Mass / _massAccumulator.MaxMass;
            accumulationBarTrans.localScale = new Vector3(xScale, accumulationBarTrans.localScale.y,
                accumulationBarTrans.localScale.z);
        }

        private void Reset()
        {
            accumulationBarTrans.localScale = new Vector3(0f, accumulationBarTrans.localScale.y,
                accumulationBarTrans.localScale.z);
        }
    }
}