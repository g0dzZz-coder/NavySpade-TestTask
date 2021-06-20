using UnityEngine;

namespace NavySpade.Entities
{
    using Player;

    public class Hero : EntityBase<HeroData>
    {
        [SerializeField] private HeroHealthController healthController = null;
        [SerializeField] private HeroMovementController movementController = null;

        public HeroHealthController HealthController => healthController;

        protected override void Awake()
        {
            base.Awake();

            Collider.isTrigger = false;
            GetComponent<Rigidbody>().useGravity = false;

            Level.Instance.Restarted += ResetState;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
                OnContactWithEnemy(enemy);
            if (other.TryGetComponent(out Crystal crystal))
                OnContactWithCrystal(crystal);
        }

        private void OnContactWithEnemy(Enemy enemy)
        {
            if (healthController.IsInvulnerable)
                return;

            healthController.TakeDamage(enemy.data.damage);
            enemy.Destroy();
        }

        private void OnContactWithCrystal(Crystal crystal)
        {
            Player.IncreaseScore(crystal.data.GetReward());
            healthController.InscreaseHealth(crystal.data.healthReward);

            crystal.Destroy();
        }

        private void ResetState()
        {
            healthController.Init(data);
            movementController.Init(this);
        }
    }
}