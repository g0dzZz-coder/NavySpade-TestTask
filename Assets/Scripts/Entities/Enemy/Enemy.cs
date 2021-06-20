using UnityEngine;
using UnityEngine.AI;

namespace NavySpade.Entities
{
    public class Enemy : EntityBase<EnemyData>
    {
        [SerializeField] private NavMeshAgent agent = null;

        private float timer = 0f;

        private void Start()
        {
            agent.speed = data.speed;
        }

        private void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;

            if (timer < data.wanderDuration)
                return;

            var target = GetRandomNavmeshLocation(data.wanderRadius, -1);
            Debug.Log(target);
            agent.SetDestination(target);
            timer = 0;
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