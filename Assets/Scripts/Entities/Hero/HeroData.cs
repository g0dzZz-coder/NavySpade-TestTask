using UnityEngine;

namespace NavySpade.Entities
{
    [CreateAssetMenu(fileName = "Hero", menuName = "Settings/Hero", order = 51)]
    public class HeroData : EntityData
    {
        [Header("Stats")]
        [Range(0, 3)]
        public int startHealth = 3;
        [Range(3, 10)]
        public int maxHealth = 3;
        public int speed = 3;
        public float durationOfInvulnerability = 3f;
    }
}