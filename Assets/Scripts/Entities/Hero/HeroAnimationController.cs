using UnityEngine;

namespace NavySpade.Entities.Hero
{
    [RequireComponent(typeof(Animator))]
    public class HeroAnimationController : MonoBehaviour
    {
        [SerializeField] private HeroMovementController movementController = null;
        [SerializeField] private Animator animator = null;

        private void OnEnable()
        {
            movementController.MoveStarted += OnMoveStarted;
            movementController.MoveEnded += OnMoveEnded;
        }

        private void OnDisable()
        {
            movementController.MoveStarted -= OnMoveStarted;
            movementController.MoveEnded -= OnMoveEnded;
        }

        public void OnMoveStarted()
        {
            animator.SetBool("IsRun", true);
        }

        public void OnMoveEnded()
        {
            animator.SetBool("IsRun", false);
        }
    }
}