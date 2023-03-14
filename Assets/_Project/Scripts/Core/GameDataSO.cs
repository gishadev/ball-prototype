using UnityEngine;

namespace Gisha.BallGame.Core
{
    [CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 0)]
    public class GameDataSO : ScriptableObject
    {
        [SerializeField] private float initialShootMass = 0.1f;
        [Header("Projectile")]
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float projectileFlySpeed = 1f;

        public float InitialShootMass => initialShootMass;

        public float ProjectileFlySpeed => projectileFlySpeed;

        public GameObject ProjectilePrefab => projectilePrefab;
    }
}
