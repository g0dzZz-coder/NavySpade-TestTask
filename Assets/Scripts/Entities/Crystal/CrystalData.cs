using UnityEngine;

namespace NavySpade.Entities
{
    [CreateAssetMenu(fileName = "Crystal", menuName = "Settings/Crystal", order = 51)]
    public class CrystalData : SpawnableEntityData
    {
        [MinMaxSlider(0f, 60f)]
        [SerializeField] private Vector2 spawnDelay = new Vector2(5f, 5f);

        [Header("Rewards")]
        public int healthReward = 1;
        [MinMaxSlider(0, 10)]
        [SerializeField] private Vector2Int scoreReward = new Vector2Int(1, 10);

        public int GetReward()
        {
            return Random.Range(scoreReward.x, scoreReward.y);
        }

        public float GetSpawnDelay()
        {
            return Random.Range(spawnDelay.x, spawnDelay.y);
        }
    }
}