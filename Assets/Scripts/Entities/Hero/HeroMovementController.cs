using System;
using UnityEngine;

namespace NavySpade.Entities.Hero
{
    using Map;

    [RequireComponent(typeof(HeroInputReceiver))]
    [RequireComponent(typeof(IMovementHandler))]
    public class HeroMovementController : MonoBehaviour
    {
        [SerializeField] private float stoppingDistance = 0.25f;
        [SerializeField] private Tile startPoint = null;

        public Hero Source { get; private set; }

        public event Action MoveStarted;
        public event Action MoveEnded;

        private bool isMoving;
        private Vector3 destination;

        private HeroInputReceiver inputReceiver;
        private IMovementHandler handler;

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

            inputReceiver = GetComponent<HeroInputReceiver>();
            inputReceiver.TargetSelected += MoveTo;

            handler = GetComponent<IMovementHandler>();
            handler.Init(Source.data.speed, stoppingDistance);
        }

        private void MoveTo(Vector3 target)
        {
            var newDestination = new Vector3(target.x, 0f, target.z);
            if (handler.TryToSetDestination(newDestination))
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
            handler.SetPosition(position);
        }
    }
}