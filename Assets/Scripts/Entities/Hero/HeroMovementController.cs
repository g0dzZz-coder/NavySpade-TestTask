using System;
using UnityEngine;
using UnityEngine.AI;

namespace NavySpade.Entities
{
    using Map;

    [RequireComponent(typeof(HeroInputReceiver))]
    public class HeroMovementController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent = null;
        [SerializeField] private float stoppingDistance = 0.25f;
        [SerializeField] private Tile startPoint = null;

        public Hero Source { get; private set; }

        public event Action MoveStarted;
        public event Action MoveEnded;

        private bool isMoving;
        private Vector3 destination;
        private HeroInputReceiver inputReceiver;

        private void Update()
        {
            if (isMoving == false)
                return;

            var distance = Vector3.Distance(destination, transform.position);
            if (distance < stoppingDistance)
                StopMove();
        }

        public void Init(Hero source)
        {
            Source = source;

            ResetPosition();

            agent.speed = Source.data.speed;
            agent.stoppingDistance = stoppingDistance;

            inputReceiver = GetComponent<HeroInputReceiver>();
            inputReceiver.TargetSelected += MoveTo;
        }

        private void MoveTo(Vector3 target)
        {
            var newDestination = new Vector3(target.x, 0f, target.z);
            if (agent.SetDestination(newDestination))
            {
                destination = newDestination;
                isMoving = true;

                MoveStarted?.Invoke();
            }
        }

        private void StopMove()
        {
            isMoving = false;
            MoveEnded?.Invoke();
        }

        private void ResetPosition()
        {
            if (startPoint == null)
                return;

            StopMove();

            var position = new Vector3(startPoint.transform.position.x, 0f, startPoint.transform.position.z);
            transform.position = position;
            agent.SetDestination(position);
        }
    }
}