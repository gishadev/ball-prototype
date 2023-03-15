using System.Linq;
using UnityEngine;

namespace Gisha.BallGame.World
{
    [ExecuteInEditMode]
    public class PathBuilder : MonoBehaviour
    {
        [SerializeField] private PathPoint[] points;
        [SerializeField] private LineRenderer lineRenderer;
        [Space] [SerializeField] private Transform trophyTrans;
        [SerializeField] private Transform playerBallTrans;


        private PathLineBuilder _pathLineBuilder;

        private void Awake()
        {
            _pathLineBuilder = new PathLineBuilder(points, lineRenderer);
        }

        private void Start()
        {
            _pathLineBuilder.Build();
        }

        private void Update()
        {
            if (Application.isPlaying)
                return;

            GeneratePath();
        }

        private void GeneratePath()
        {
            _pathLineBuilder.Build();

            var sPoint = points.FirstOrDefault(x => x is StartPoint);
            playerBallTrans.position = new Vector3(sPoint.transform.position.x, playerBallTrans.transform.position.y,
                sPoint.transform.position.z);

            var fPoint = points.FirstOrDefault(x => x is FinishPoint);
            trophyTrans.position = new Vector3(fPoint.transform.position.x, trophyTrans.transform.position.y,
                fPoint.transform.position.z);
        }
    }

    public class PathLineBuilder
    {
        private readonly PathPoint[] _points;
        private readonly LineRenderer _lineRenderer;

        public PathLineBuilder(PathPoint[] points, LineRenderer lineRenderer)
        {
            _points = points;
            _lineRenderer = lineRenderer;
        }

        public void Build()
        {
            var positions = _points
                .Select(x => x.transform.position)
                .ToArray();

            _lineRenderer.positionCount = positions.Length;
            _lineRenderer.SetPositions(positions);
        }
    }
}