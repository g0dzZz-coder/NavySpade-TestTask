using UnityEngine;
using UnityEngine.AI;

namespace NavySpade.Entities
{
    [RequireComponent(typeof(IMovementHandler))]
    public class Enemy : EntityBase<EnemyData>
    {
        private IMovementHandler movementHandler;

        private float timer = 0f;

        protected override void Awake()
        {
            base.Awake();
            movementHandler = GetComponent<IMovementHandler>();
            movementHandler.Init(data.speed, 0f);
        }

        private void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;

            if (timer < data.wanderDuration)
                return;

            var target = GetRandomNavmeshLocation(data.wanderRadius, -1);         
            if (movementHandler.TryToSetDestination(target))
            {
                timer = 0;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Crystal crystal))
                OnContactWithCrystal(crystal);
        }

        private void OnContactWithCrystal(Crystal crystal)
        {
            crystal.Destroy();
            Destroy();
        }

        private Vector3 GetRandomNavmeshLocation(float radius, LayerMask layerMask)
        {
            var randomDirection = Random.insideUnitSphere * radius;
            randomDirection += transform.position;
            var finalPosition = Vector3.zero;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, layerMask))
                finalPosition = hit.position;

            return finalPosition;
        }
    }
}