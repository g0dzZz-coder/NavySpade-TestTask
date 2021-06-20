using UnityEngine;

namespace NavySpade.Entities
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Settings/Enemy", order = 51)]
    public class EnemyData : SpawnableEntityData
    {
        [Header("Stats")]
        [Range(0f, 5f)]
        public float speed = 1f;
        [Range(1, 3)]
        public int damage = 1;
        public float wanderRadius = 4f;
        public float wanderDuration = 10f;
    }
}