using UnityEngine;

namespace NavySpade.Entities.Hero
{
    [CreateAssetMenu(fileName = "Hero", menuName = "Settings/Hero", order = 51)]
    public class HeroData : EntityData
    {
        [Header("Stats")]
        [Range(0, 3)]
        public int startHealth = 3;
        [Range(3, 10)]
        public int maxHealth = 3;
        [Range(0f, 5f)]
        public float speed = 3f;
        [Range(0.1f, 10f)]
        public float durationOfInvulnerability = 3f;
    }
}