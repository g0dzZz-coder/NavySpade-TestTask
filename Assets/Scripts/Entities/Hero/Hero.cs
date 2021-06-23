using UnityEngine;

namespace NavySpade.Entities.Hero
{
    using Player;

    public class Hero : EntityBase<HeroData>
    {
        [SerializeField] private HeroHealthController _healthController = null;
        [SerializeField] private HeroMovementController _movementController = null;

        public HeroHealthController HealthController => _healthController;

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
            if (_healthController.IsInvulnerable)
                return;

            _healthController.TakeDamage(enemy.data.damage);
            enemy.Destroy();
        }

        private void OnContactWithCrystal(Crystal crystal)
        {
            Player.IncreaseScore(crystal.data.GetReward());
            _healthController.InscreaseHealth(crystal.data.healthReward);

            crystal.Destroy();
        }

        private void ResetState()
        {
            _healthController.Init(data);
            _movementController.Init(this);
        }
    }
}