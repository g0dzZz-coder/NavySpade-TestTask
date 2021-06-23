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
        [SerializeField] private NavMeshAgent _agent = null;

        public void Init(float speed, float stoppingDistance)
        {
            _agent.speed = speed;
            _agent.stoppingDistance = stoppingDistance;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
            //_agent.SetDestination(position);
        }

        public bool TryToSetDestination(Vector3 destination)
        {
            if (_agent.SetDestination(destination))
                return true;

            return false;
        }
    }
}