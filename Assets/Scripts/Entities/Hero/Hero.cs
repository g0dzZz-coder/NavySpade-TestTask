using System.Collections;
using UnityEngine;

namespace NavySpade.Entities
{
    using Map;

    public class Hero : EntityBase<HeroData>
    {
        [SerializeField] private HeroMovementController movementController = null;
        [SerializeField] private Tile startPosition = null;

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
            else if (other.TryGetComponent(out ICollectable collectable))
                collectable.OnCollect();
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