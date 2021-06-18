using UnityEngine;

namespace NavySpade.Gameplay.Entities
{
    public class CrystalData : SpawnableEntityData
    {
        public bool isRandomSpawnTime = true;

        public int healthRestored = 1;
        public Vector2 minMaxScoreReward = new Vector2(1, 10);
    }
}