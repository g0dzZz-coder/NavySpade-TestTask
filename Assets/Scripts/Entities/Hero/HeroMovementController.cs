using System;
using UnityEngine;

namespace NavySpade.Entities.Hero
{
    [RequireComponent(typeof(HeroInputReceiver))]
    [RequireComponent(typeof(IMovementHandler))]
    public class HeroMovementController : MonoBehaviour
    {
        [SerializeField] private float _stoppingDistance = 0.25f;

        public Hero Source { get; private set; }

        public event Action MoveStarted;
        public event Action MoveEnded;

        private bool _isMoving;
        private Vector3 _destination;

        private HeroInputReceiver _inputReceiver;
        private IMovementHandler _handler;

        private void Update()
        {
            if (_isMoving == false)
                return;

            var distance = Vector3.Distance(_destination, transform.position);
            if (distance < _stoppingDistance)
                StopMove();
        }

        public void Init(Hero source)
        {
            Source = source;

            _inputReceiver = GetComponent<HeroInputReceiver>();
            _inputReceiver.TargetSelected += MoveTo;

            _handler = GetComponent<IMovementHandler>();
            _handler.Init(Source.data.speed, _stoppingDistance);

            ResetPosition();
        }

        private void MoveTo(Vector3 target)
        {
            var newDestination = new Vector3(target.x, 0f, target.z);
            if (_handler.TryToSetDestination(newDestination))
            {
                _destination = newDestination;
                _isMoving = true;

                MoveStarted?.Invoke();
            }
        }

        private void StopMove()
        {
            _isMoving = false;

            MoveEnded?.Invoke();
        }

        private void ResetPosition()
        {
            var startPoint = Level.Instance.GetHeroStartPoint();
            if (startPoint == null)
                return;

            StopMove();

            var position = new Vector3(startPoint.transform.position.x, 0f, startPoint.transform.position.z);
            _handler.SetPosition(position);
        }
    }
}