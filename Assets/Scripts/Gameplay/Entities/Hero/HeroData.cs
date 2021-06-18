using System;
using UnityEngine;

namespace NavySpade.Gameplay.Entities
{
    [CreateAssetMenu(fileName = "HeroData", menuName = "Models/Hero", order = 52)]
    public class HeroData : EntityData
    {
        public int Lives;
        public int Speed;

        public event Action<int> LivesUpdated;
        public event Action<int> SpeedUpdated;

        public void RemoveLives(int amount)
        {
            if (amount < 1)
                return;

            Lives -= amount;
            if (Lives < 0)
                Lives = 0;

            LivesUpdated?.Invoke(Lives);
        }

        public void SetSpeed(int value)
        {
            if (value < 0)
                return;

            Speed = value;

            SpeedUpdated?.Invoke(Speed);
        }
    }
}