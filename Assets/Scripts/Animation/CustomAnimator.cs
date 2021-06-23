using UnityEngine;

namespace NavySpade.Animation
{
    public class CustomAnimator : MonoBehaviour
    {
        [SerializeField] private AnimationSettings _settings = null;

        public AnimationSettings Settings => _settings;

        private void OnEnable()
        {
            Show();
        }

        public void Show()
        {
            AnimationExtensions.Show(transform);
        }

        public void Hide()
        {
            AnimationExtensions.Hide(transform);
        }
    }
}