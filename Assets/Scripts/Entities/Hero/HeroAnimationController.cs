using UnityEngine;

namespace NavySpade.Entities.Hero
{
    [RequireComponent(typeof(Animator))]
    public class HeroAnimationController : MonoBehaviour
    {
        [SerializeField] private HeroMovementController _movementController = null;
        [SerializeField] private Animator _animator = null;

        private void OnEnable()
        {
            _movementController.MoveStarted += OnMoveStarted;
            _movementController.MoveEnded += OnMoveEnded;
        }

        private void OnDisable()
        {
            _movementController.MoveStarted -= OnMoveStarted;
            _movementController.MoveEnded -= OnMoveEnded;
        }

        public void OnMoveStarted()
        {
            _animator.SetBool("IsRun", true);
        }

        public void OnMoveEnded()
        {
            _animator.SetBool("IsRun", false);
        }
    }
}