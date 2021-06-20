using UnityEngine;
using UnityEngine.AI;

namespace NavySpade.Entities
{
    public interface IMovementHandler
    {
        void Init(float speed, float stoppingDistance);
        bool TryToSetDestination(Vector3 destination);
        void SetPosition(Vector3 position);
    }

    public class NavMeshMovementHandler : MonoBehaviour, IMovementHandler
    {
        [SerializeField] private NavMeshAgent agent = null;

        public void Init(float speed, float stoppingDistance)
        {
            agent.speed = speed;
            agent.stoppingDistance = stoppingDistance;
        }

        public void SetPosition(Vector3 position)
        {
            agent.SetDestination(position);
        }

        public bool TryToSetDestination(Vector3 destination)
        {
            if (agent.SetDestination(destination))
                return true;

            return false;
        }
    }
}