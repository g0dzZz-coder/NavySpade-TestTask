using DG.Tweening;
using System;
using UnityEngine;

namespace NavySpade.Animation
{
    public static class CustomAnimator
    {
        public static void Show(Transform transform, AnimationSettings settings, Action callback = null)
        {
            transform.gameObject.SetActive(false);
            transform.DOScale(0f, 0f);
            transform.gameObject.SetActive(true);

            if (settings == null)
                return;

            transform.DOScale(1f, settings.time).OnComplete(() => callback?.Invoke());
        }

        public static void Hide(Transform transform, AnimationSettings settings, Action callback = null)
        {
            if (settings == null)
            {
                OnAnimationCompleted(transform.gameObject, callback);
                return;
            }

            transform.DOScale(0f, settings.time).OnComplete(() => OnAnimationCompleted(transform.gameObject, callback));
        }

        public static void Show(CanvasGroup canvasGroup, AnimationSettings settings, Action callback = null)
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

        public static void Hide(CanvasGroup canvasGroup, AnimationSettings settings, Action callback = null)
        {
            if (settings == null)
            {
                OnAnimationCompleted(canvasGroup.gameObject, callback);
                return;
            }

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