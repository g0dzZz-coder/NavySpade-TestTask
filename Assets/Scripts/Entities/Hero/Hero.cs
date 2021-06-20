using System.Collections;
using UnityEngine;
using System;

namespace NavySpade.Entities
{
    using Map;
    using Core;

    public class Hero : EntityBase<HeroData>
    {
        [SerializeField] private HeroMovementController movementController = null;
        [SerializeField] private Tile startPosition = null;

        public int Health { get; private set; }

        public event Action<int> HealthUpdated;

        private bool isInvulnerable = false;

        private void Awake()
        {
            Health = data.startHealth;
            HealthUpdated?.Invoke(Health);
        }

        private void Start()
        {
            movementController.Init(this);

            if (startPosition)
                transform.position = new Vector3(startPosition.transform.position.x, 0f, startPosition.transform.position.z);

            GetComponent<Collider>().isTrigger = true;
            GetComponent<Rigidbody>().useGravity = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
                OnContactWithEnemy(enemy);
            else if (other.TryGetComponent(out Crystal crystal))
                OnContactWithCrystal(crystal);
        }

        private void TakeDamage(int value)
        {
            if (isInvulnerable || value < 1)
                return;

            ReduceHealth(value);

            if (isInvulnerable)
                return;

            StartCoroutine(EnableInvulnerability());
        }

        private void InscreaseHealth(int value)
        {
            if (value < 1)
                return;

            Health += value;
            if (Health > data.maxHealth)
                Health = data.maxHealth;

            HealthUpdated?.Invoke(Health);
        }

        private void ReduceHealth(int value)
        {
            if (value < 1)
                return;

            Health -= value;
            if (Health < 0)
                Health = 0;

            HealthUpdated?.Invoke(Health);
        }

        private void OnContactWithEnemy(Enemy enemy)
        {
            TakeDamage(enemy.data.damage);
            isInvulnerable = true;
        }

        private void OnContactWithCrystal(Crystal crystal)
        {
            Player.IncreaseScore(crystal.data.GetReward());
            InscreaseHealth(crystal.data.healthReward);
            Destroy(crystal.gameObject);
        }

        private IEnumerator EnableInvulnerability()
        {
            isInvulnerable = true;

            var duration = 0f;
            while (duration < data.durationOfInvulnerability)
            {
                duration += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            isInvulnerable = false;
        }
    }
}