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
        
        private PathLineBuilder _pathLineBuilder;

        public PathPoint[] Points => points;

        private void Awake()
        {
            _pathLineBuilder = new PathLineBuilder(Points, lineRenderer);
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
            if (_pathLineBuilder == null)
                _pathLineBuilder = new PathLineBuilder(Points, lineRenderer);

            _pathLineBuilder.Build();
            
            var fPoint = Points.FirstOrDefault(x => x is FinishPoint);
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