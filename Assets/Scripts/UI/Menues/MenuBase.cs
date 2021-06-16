using UnityEngine;
using UnityEngine.Events;

namespace NavySpade.UI
{
    using Animation;
    using Utils;

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

            if (animationSettings)
                panel.Show(animationSettings);

            onEnabled.Invoke();
        }

        public virtual void Disable()
        {
            if (panel.enabled == false)
                return;

            if (animationSettings)
            {
                panel.Hide(animationSettings, () => OnDisabled());
                return;
            }

            onDisabled.Invoke();
        }

        private void OnDisabled()
        {
            panel.gameObject.SetActive(false);
            onDisabled?.Invoke();
        }
    }
}