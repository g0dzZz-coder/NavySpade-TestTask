using UnityEngine;
using UnityEngine.AI;

namespace NavySpade.Entities
{
    public class Enemy : EntityBase<EnemyData>
    {
        [SerializeField] private NavMeshAgent agent = null;

        private float timer = 0f;

        private void Awake()
        {
            agent.speed = data.speed;
        }

        private void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;

            if (timer < data.wanderDuration)
                return;

            var target = GetRandomNavmeshLocation(data.wanderRadius, -1);
            agent.SetDestination(target);
            timer = 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HeroHealthController hero))
                OnContactWithHero(hero);
            else if (other.TryGetComponent(out Crystal crystal))
                OnContactWithCrystal(crystal);
        }

        private void OnContactWithHero(HeroHealthController hero)
        {
            hero.TakeDamage(data.damage);
            Destroy();
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