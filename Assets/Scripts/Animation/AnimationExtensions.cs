using DG.Tweening;
using System;
using UnityEngine;

namespace NavySpade.Animation
{
    public static class AnimationExtensions
    {
        public static void Show(Transform obj, Action callback = null)
        {
            if (obj == null)
                return;

            obj.gameObject.SetActive(true);

            if (obj.TryGetComponent(out CustomAnimator customAnimator))
            {
                ShowUsingDotween(obj, customAnimator.Settings, callback);
            }
            else if (obj.TryGetComponent(out Animator animator))
            {
                ShowUsingAnimator(animator);
                callback?.Invoke();
            }
        }

        public static void Hide(Transform obj, Action callback = null)
        {
            if (obj == null)
                return;

            if (obj.TryGetComponent(out CustomAnimator customAnimator))
            {
                HideUsingDotween(obj, customAnimator.Settings, callback);
                return;
            }

            if (obj.TryGetComponent(out Animator animator))
                HideUsingAnimator(animator);
            else
                obj.gameObject.SetActive(false);

            callback?.Invoke();
        }

        private static void ShowUsingAnimator(Animator animator)
        {
            animator.SetBool("IsOpen", true);
        }

        private static void HideUsingAnimator(Animator animator)
        {
            animator.SetBool("IsOpen", false);
        }

        private static void ShowUsingDotween(Transform transform, AnimationSettings settings, Action callback = null)
        {
            if (transform.TryGetComponent(out CanvasGroup canvasGroup))
            {
                canvasGroup.alpha = 0f;
                canvasGroup.DOFade(100f, settings.duration).OnComplete(() => callback?.Invoke());
            }
            else
            {
                transform.DOScale(0f, 0f);
                transform.DOScale(1f, settings.duration).OnComplete(() => callback?.Invoke());
            }
        }

        private static void HideUsingDotween(Transform transform, AnimationSettings settings, Action callback = null)
        {
            if (transform.TryGetComponent(out CanvasGroup canvasGroup))
                canvasGroup.DOFade(0f, settings.duration).OnComplete(() => OnHideAnimationCompleted(canvasGroup.gameObject, callback));
            else
                transform.DOScale(0f, settings.duration).OnComplete(() => OnHideAnimationCompleted(transform.gameObject, callback));
        }

        private static void OnHideAnimationCompleted(GameObject obj, Action callback = null)
        {
            obj.SetActive(false);
            callback?.Invoke();
        }
    }
}