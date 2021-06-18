using UnityEngine;

namespace NavySpade.Animation
{
    public class CustomAnimator : MonoBehaviour
    {
        [SerializeField] private AnimationSettings settings = null;

        public AnimationSettings Settings => settings;

        private void Start()
        {
            AnimationExtensions.Show(transform);
        }
    }
}