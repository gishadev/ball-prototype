using Gisha.BallGame.Core;
using UnityEngine;

namespace Gisha.BallGame.Ball
{
    [RequireComponent(typeof(LineRenderer))]
    public class TrajectoryVisual : MonoBehaviour
    {
        [SerializeField] private float maxLength = 5f;

        private LineRenderer _lineRenderer;
        private IWorldTouchController _touchController;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            _touchController = GameManager.Instance.WorldTouchController;

            _touchController.WorldTouchDown += OnTouchDown;
            _touchController.WorldTouchUp += OnTouchUp;
        }

        private void OnDisable()
        {
            _touchController.WorldTouchDown -= OnTouchDown;
            _touchController.WorldTouchUp -= OnTouchUp;
        }

        private void OnTouchUp(Vector3 pos)
        {
            _lineRenderer.enabled = false;
        }

        private void OnTouchDown(Vector3 pos)
        {
            _lineRenderer.enabled = true;
        }

        private void Update()
        {
            if (!_touchController.IsFingerDown)
                return;

            BuildTrajectoryLine();
        }

        private void BuildTrajectoryLine()
        {
            var endPos = _touchController.FingerWorldPosition - transform.position;
            endPos.y = 0f;

            if (endPos.magnitude > maxLength)
                endPos = Vector3.ClampMagnitude(endPos, maxLength);

            _lineRenderer.SetPosition(1, endPos);
        }
    }
}