using UnityEngine;

namespace NavySpade.Entities
{
    [CreateAssetMenu(fileName = "Crystal", menuName = "Settings/Crystal", order = 51)]
    public class CrystalData : SpawnableEntityData
    {
        [Header("Rewards")]
        public int healthReward = 1;
        [MinMaxSlider(0, 10)]
        [SerializeField] private Vector2Int scoreReward = new Vector2Int(1, 10);

        public int GetReward()
        {
            return Random.Range(scoreReward.x, scoreReward.y);
        }
    }
}