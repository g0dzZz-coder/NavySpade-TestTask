using System;
using TMPro;
using UnityEngine;

namespace NavySpade.UI
{
    using Gameplay.Entities;

    public class HeroUI : UIElement
    {
        [SerializeField] private HeroData hero = null;
        [SerializeField] private TMP_Text healthText = null;

        private void Start()
        {
            Show();
        }

        private void OnEnable()
        {
            UpdateLives(hero.Health);

            hero.HealthUpdated += UpdateLives;
        }

        private void OnDisable()
        {
            hero.HealthUpdated -= UpdateLives;
        }

        private void UpdateLives(int livesCount)
        {
            healthText.text = livesCount.ToString();
        }

        private void OnValidate()
        {
            if (healthText == null)
                throw new NullReferenceException();
        }
    }
}