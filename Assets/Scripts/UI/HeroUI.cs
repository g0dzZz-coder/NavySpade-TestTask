using TMPro;
using UnityEngine;

namespace NavySpade.UI
{
    using Entities;

    public class HeroUI : UIElement
    {
        [SerializeField] private HeroHealthController hero = null;
        [SerializeField] private TMP_Text healthText = null;

        private void Start()
        {
            UpdateLives(hero.Health);

            Level.Instance.Restarted += Show;
            Level.Instance.GameEnded += Hide;
            hero.HealthUpdated += UpdateLives;
        }

        private void UpdateLives(int livesCount)
        {
            healthText.text = livesCount.ToString();
        }
    }
}