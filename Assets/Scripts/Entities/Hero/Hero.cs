using UnityEngine;

namespace NavySpade.Entities
{
    using Player;

    public class Hero : EntityBase<HeroData>
    {
        [SerializeField] private HeroHealthController healthController = null;
        [SerializeField] private HeroMovementController movementController = null;

        public HeroHealthController HealthController => healthController;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
            GetComponent<Rigidbody>().useGravity = false;

            Level.Instance.Restarted += ResetState;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Crystal crystal))
                OnContactWithCrystal(crystal);
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