using DG.Tweening;
using System;
using UnityEngine;

namespace NavySpade.Utils
{
    using Animation;

    public static class CanvasGroupExtensions
    {
        public static void Show(this CanvasGroup canvasGroup, AnimationSettings settings, Action callback = null)
        {
            canvasGroup.gameObject.SetActive(true);

            switch (settings.type)
            {
                case AnimationType.Fade:
                    canvasGroup.DOFade(100f, settings.time).OnComplete(() => callback?.Invoke());
                    break;
                case AnimationType.Scale:
                    canvasGroup.transform.DOScale(1f, settings.time).OnComplete(() => callback?.Invoke());
                    break;
            }
        }

        public static void Hide(this CanvasGroup canvasGroup, AnimationSettings settings, Action callback = null)
        {
            switch (settings.type)
            {
                case AnimationType.Fade:
                    canvasGroup.DOFade(0f, settings.time).OnComplete(() => OnAnimationCompleted(canvasGroup.gameObject));
                    break;
                case AnimationType.Scale:
                    canvasGroup.transform.DOScale(1f, settings.time).OnComplete(() => OnAnimationCompleted(canvasGroup.gameObject));
                    break;
            }
        }

        private static void OnAnimationCompleted(GameObject obj, Action callback = null)
        {
            obj.SetActive(false);
            callback?.Invoke();
        }
    }
}
