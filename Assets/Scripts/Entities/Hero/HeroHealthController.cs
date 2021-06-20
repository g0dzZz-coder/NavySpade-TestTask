using System;
using System.Collections;
using UnityEngine;

namespace NavySpade.Entities.Hero
{
    public class HeroHealthController : MonoBehaviour
    {
        public int Health { get; private set; }
        public bool IsInvulnerable { get; private set; }
        
        public event Action<int> HealthUpdated;
        public event Action Died;
        public event Action<bool> InvulnerableUpdated;

        private HeroData data;

        public void Init(HeroData data)
        {
            this.data = data;
            Health = data.startHealth;
            HealthUpdated?.Invoke(Health);

            SetInvulnerable(false);
        }

        public void TakeDamage(int value)
        {
            if (IsInvulnerable || value < 1)
                return;

            SetHealth(Health - value);
            StartCoroutine(EnableInvulnerability());
        }

        public void InscreaseHealth(int value)
        {
            if (value > 0)
                SetHealth(Health + value);
        }

        private void SetHealth(int value)
        {
            Health = value;
            if (Health < 1)
            {
                Health = 0;
                Died?.Invoke();
            }
            else if (Health > data.maxHealth)
            {
                Health = data.maxHealth;
            }

            HealthUpdated?.Invoke(Health);
        }

        private IEnumerator EnableInvulnerability()
        {
            SetInvulnerable(true);

            var duration = 0f;
            while (duration < data.durationOfInvulnerability)
            {
                duration += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            SetInvulnerable(false);
        }

        private void SetInvulnerable(bool enable)
        {
            IsInvulnerable = enable;
            InvulnerableUpdated?.Invoke(IsInvulnerable);
        }
    }
}