using UnityEngine;

namespace Gisha.BallGame.Core
{
    [CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 0)]
    public class GameDataSO : ScriptableObject
    {
        [SerializeField] private float initialShootMass = 0.1f;

        public float InitialShootMass => initialShootMass;
    }
}
