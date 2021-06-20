using UnityEngine;

namespace NavySpade.Entities
{
    [CreateAssetMenu(fileName = "SpawnableEntity", menuName = "Settings/SpawnableEntity", order = 51)]
    public class SpawnableEntityData : EntityData
    {
        [Header("Spawn")]
        public int startAmount = 10;
        [MinMaxSlider(0f, 60f)]
        [SerializeField] private Vector2 spawnDelay = new Vector2(5f, 5f);

        public float GetSpawnDelay()
        {
            return Random.Range(spawnDelay.x, spawnDelay.y);
        }
    }
}