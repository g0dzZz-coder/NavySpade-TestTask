using UnityEngine;

namespace NavySpade.Entities
{
    [CreateAssetMenu(fileName = "Obstacle", menuName = "Settings/Entity", order = 51)]
    public class SpawnableEntityData : EntityData
    {
        [Header("Spawn")]
        public int startAmount = 10;
    }
}