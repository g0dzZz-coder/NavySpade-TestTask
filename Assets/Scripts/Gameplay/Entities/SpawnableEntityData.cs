using UnityEngine;

namespace NavySpade.Gameplay.Entities
{
    [CreateAssetMenu(fileName = "Obstacle", menuName = "Settings/Entity", order = 51)]
    public class SpawnableEntityData : EntityData
    {
        public float spawnDelay = 5f;
        public bool randomDelay = false;
        public int maxCount = 10;
    }
}