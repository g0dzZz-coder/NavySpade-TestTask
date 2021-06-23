using UnityEngine;
using UnityEngine.AI;

namespace NavySpade.Entities
{
    [RequireComponent(typeof(IMovementHandler))]
    public class Enemy : EntityBase<EnemyData>
    {
        private float _timer = 0f;
        private IMovementHandler _movementHandler;

        protected override void Awake()
        {
            base.Awake();

            _movementHandler = GetComponent<IMovementHandler>();
            _movementHandler.Init(data.speed, 0f);
        }

        private void FixedUpdate()
        {
            _timer += Time.fixedDeltaTime;

            if (_timer < data.wanderDuration)
                return;

            var target = GetRandomNavmeshLocation(data.wanderRadius, -1);         
           
            if (_movementHandler.TryToSetDestination(target))
                _timer = 0;
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