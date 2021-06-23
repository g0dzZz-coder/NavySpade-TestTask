using TMPro;
using UnityEngine;

namespace NavySpade.UI
{
    using Animation;
    using Entities.Hero;

    public class HeroUI : UIElement
    {
        [SerializeField] private HeroHealthController _hero = null;
        [SerializeField] private TMP_Text _healthText = null;
        [SerializeField] private Transform _invulnerability = null;

        private void Start()
        {
            UpdateLives(_hero.Health);
            UpdateInvulnerable(_hero.IsInvulnerable);

            Level.Instance.Restarted += Show;
            Level.Instance.GameEnded += Hide;

            _hero.HealthUpdated += UpdateLives;
            _hero.InvulnerableUpdated += UpdateInvulnerable;
        }

        private void UpdateLives(int livesCount)
        {
            _healthText.text = livesCount.ToString();
        }

        private void UpdateInvulnerable(bool enabled)
        {
            if (enabled)
                AnimationExtensions.Show(_invulnerability);
            else
                AnimationExtensions.Hide(_invulnerability);
        }
    }
}