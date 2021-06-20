using UnityEngine;

namespace NavySpade.Entities
{
    [RequireComponent(typeof(Animator))]
    public class HeroAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator = null;

        public void OnStartRun()
        {
            animator.SetBool("IsRun", true);
        }

        public void OnStopRun()
        {
            animator.SetBool("IsRun", false);
        }
    }
}