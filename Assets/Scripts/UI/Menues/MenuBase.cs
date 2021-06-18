using UnityEngine;
using UnityEngine.Events;

namespace NavySpade.UI
{
    using Animation;

    public abstract class MenuBase : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup panel = null;
        [SerializeField] protected AnimationSettings animationSettings = null;

        [SerializeField] protected UnityEvent onEnabled = null;
        [SerializeField] protected UnityEvent onDisabled = null;

        public virtual void Enable()
        {
            if (panel.enabled)
                return;

            CustomAnimator.Show(panel, animationSettings, () => onEnabled.Invoke());
        }

        public virtual void Disable()
        {
            if (panel.enabled == false)
                return;

            CustomAnimator.Hide(panel, animationSettings, OnDisabled);
        }

        private void OnDisabled()
        {
            panel.gameObject.SetActive(false);
            onDisabled?.Invoke();
        }
    }
}