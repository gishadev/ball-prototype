using System.Collections;
using Gisha.BallGame.Core;
using UnityEngine;

namespace Gisha.BallGame.Ball
{
    public class PlayerBall : MonoBehaviour
    {
        [SerializeField] private float scalingSpeed = 2f;

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
            StartCoroutine(ScalingRoutine());

            if (_currentMass <= 0f)
                EventManager.TriggerEvent(Constants.EVENT_MASS_ZERO, null);
        }

        private void ApplyMass(float mass)
        {
            transform.localScale = Vector3.one * mass;
            AlignYPos();
        }

        private IEnumerator ScalingRoutine()
        {
            while (transform.localScale.x > CurrentMass)
            {
                var xScale = transform.localScale.x;
                xScale = Mathf.Lerp(xScale, CurrentMass, Time.deltaTime * scalingSpeed);
                ApplyMass(xScale);
                yield return null;
            }

            ApplyMass(CurrentMass);
        }

        private void AlignYPos()
        {
            transform.position = new Vector3(transform.position.x, transform.localScale.y / 2f, transform.position.z);
        }
    }
}