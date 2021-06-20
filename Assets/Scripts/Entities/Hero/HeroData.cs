using System;
using UnityEngine;

namespace NavySpade.Entities
{
    [CreateAssetMenu(fileName = "Hero", menuName = "Settings/Hero", order = 51)]
    public class HeroData : EntityData
    {
        [Header("Stats")]
        [Range(0, 3)]
        [SerializeField] private int health = 3;
        [SerializeField] private int speed = 3;
        public bool isInvulnerable = false;
        public float durationOfInvulnerability = 3f;

        public int Health => health;
        public int Speed => speed;

        public event Action<int> HealthUpdated;
        public event Action<int> SpeedUpdated;

        public void TakeDamage(int amount)
        {
            if (amount < 1)
                return;

            health -= amount;
            if (health < 0)
                health = 0;

            HealthUpdated?.Invoke(health);
        }

        public void SetSpeed(int value)
        {
            if (value < 0)
                return;

            speed = value;

            SpeedUpdated?.Invoke(speed);
        }
    }
}