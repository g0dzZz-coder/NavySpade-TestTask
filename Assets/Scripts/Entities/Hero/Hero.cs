using System.Collections;
using UnityEngine;

namespace NavySpade.Entities
{
    public class Hero : EntityBase<HeroData>
    {
        [SerializeField] private HeroMovementController movementController = null;

        private void Start()
        {
            movementController.Init(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy) == false)
                return;

            TakeDamage(enemy.data.damage);
        }

        private void TakeDamage(int amount)
        {
            data.TakeDamage(amount);

            if (data.isInvulnerable)
                return;

            StartCoroutine(EnableInvulnerability());
        }

        private void OnContactWithEnemy(Enemy enemy)
        {
            TakeDamage(enemy.data.damage);
            data.isInvulnerable = true;
        }

        private IEnumerator EnableInvulnerability()
        {
            data.isInvulnerable = true;
            var duration = 0f;

            while (duration < data.durationOfInvulnerability)
            {
                duration += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            data.isInvulnerable = false;
        }
    }
}