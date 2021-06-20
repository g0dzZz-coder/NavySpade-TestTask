using TMPro;
using UnityEngine;

namespace NavySpade.UI
{
    using Animation;
    using Entities.Hero;

    public class HeroUI : UIElement
    {
        [SerializeField] private HeroHealthController hero = null;
        [SerializeField] private TMP_Text healthText = null;
        [SerializeField] private Transform invulnerability = null;

        private void Start()
        {
            UpdateLives(hero.Health);

            Level.Instance.Restarted += Show;
            Level.Instance.GameEnded += Hide;

            hero.HealthUpdated += UpdateLives;
            hero.InvulnerableUpdated += UpdateInvulnerable;
        }

        private void UpdateLives(int livesCount)
        {
            healthText.text = livesCount.ToString();
        }

        private void UpdateInvulnerable(bool enabled)
        {
            if (enabled)
                AnimationExtensions.Show(invulnerability);
            else
                AnimationExtensions.Hide(invulnerability);
        }
    }
}