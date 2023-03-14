using UnityEngine;

namespace Gisha.BallGame.Ball
{
    [RequireComponent(typeof(Projectile), typeof(TrailRenderer))]
    public class ProjectileTrailRendererFixer : MonoBehaviour
    {
        private TrailRenderer _trailRenderer;

        private void Awake()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
        }

        private void Start()
        {
            _trailRenderer.startWidth = transform.localScale.x;
        }
    }
}