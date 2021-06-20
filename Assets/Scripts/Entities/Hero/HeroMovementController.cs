using UnityEngine;
using UnityEngine.AI;

namespace NavySpade.Entities
{
    [RequireComponent(typeof(HeroInputReceiver))]
    public class HeroMovementController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navAgent = null;
        [SerializeField] private HeroAnimationController animator = null;
        [SerializeField] private float stopDistance = 0.1f;

        public Hero Source { get; private set; }

        private Vector3 destination;
        private HeroInputReceiver inputReceiver = null;

        private void Update()
        {
            var distance = Vector3.Distance(destination, transform.position);
            if (distance < stopDistance)
                StopRun();
        }

        public void Init(Hero source)
        {
            Source = source;

            inputReceiver = GetComponent<HeroInputReceiver>();
            inputReceiver.TargetSelected += OnTargetSelected;

            navAgent.speed = Source.data.Speed;
        }

        private void OnTargetSelected(Vector3 position)
        {
            MoveTo(position);
        }

        private void MoveTo(Vector3 target)
        {
            destination = new Vector3(target.x, 0f, target.z);
            navAgent.SetDestination(destination);

            animator.OnStartRun();
        }

        private void StopRun()
        {
            animator.OnStopRun();
        }
    }
}