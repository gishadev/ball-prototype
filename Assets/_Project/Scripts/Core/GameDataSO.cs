using UnityEngine;

namespace Gisha.BallGame.Core
{
    [CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 0)]
    public class GameDataSO : ScriptableObject
    {
        [SerializeField] private float minAccumulationMass = 0.1f;
        [SerializeField] private float massAccumulationSpeed = 2f;
        
        [Header("Projectile")]
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float projectileFlySpeed = 1f;
        [SerializeField] private float projectileAdditionalMass = 0.5f;
        

        public GameObject ProjectilePrefab => projectilePrefab;
        public float MinAccumulationMass => minAccumulationMass;
        public float ProjectileFlySpeed => projectileFlySpeed;
        public float ProjectileAdditionalMass => projectileAdditionalMass;
        public float MassAccumulationSpeed => massAccumulationSpeed;
    }
}
