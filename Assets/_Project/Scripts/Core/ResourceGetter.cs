using UnityEngine;

namespace Gisha.BallGame.Core
{
    public static class ResourceGetter
    {
        public static GameDataSO GetGameData()
        {
            return Resources.Load<GameDataSO>("GameData");
        }
    }
}