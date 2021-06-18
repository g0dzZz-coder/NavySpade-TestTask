using UnityEngine;
using UnityEngine.Events;

namespace NavySpade.UI
{
    using Animation;

    public abstract class MenuBase : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup menuRoot = null;

        [SerializeField] protected UnityEvent onEnabled = null;
        [SerializeField] protected UnityEvent onDisabled = null;

        public virtual void Enable()
        {
            if (menuRoot.enabled)
                return;

            AnimationExtensions.Show(menuRoot.transform, () => onEnabled.Invoke());
        }

        public virtual void Disable()
        {
            if (menuRoot.enabled == false)
                return;

            AnimationExtensions.Hide(menuRoot.transform, OnDisabled);
        }

        private void OnDisabled()
        {
            menuRoot.gameObject.SetActive(false);
            onDisabled?.Invoke();
        }
    }
}