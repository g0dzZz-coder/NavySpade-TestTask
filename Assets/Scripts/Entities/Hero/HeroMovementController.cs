using UnityEngine;
using UnityEngine.AI;

namespace NavySpade.Entities
{
    [RequireComponent(typeof(HeroInputReceiver))]
    public class HeroMovementController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navAgent = null;
        [SerializeField] private HeroAnimationController animator = null;
        [SerializeField] private float stoppingDistance = 0.25f;

        public Hero Source { get; private set; }

        private Vector3 destination;
        private HeroInputReceiver inputReceiver = null;

        private void Update()
        {
            var distance = Vector3.Distance(destination, transform.position);
            if (distance < stoppingDistance)
                StopRun();
        }

        public void Init(Hero source)
        {
            Source = source;

            inputReceiver = GetComponent<HeroInputReceiver>();
            inputReceiver.TargetSelected += MoveTo;

            navAgent.speed = Source.data.Speed;
            navAgent.stoppingDistance = stoppingDistance;
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