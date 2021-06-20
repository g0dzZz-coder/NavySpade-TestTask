using UnityEngine;

namespace NavySpade.Entities
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Settings/Enemy", order = 51)]
    public class EnemyData : SpawnableEntityData
    {
        public float speed = 1f;
        public int damage = 1;
    }
}