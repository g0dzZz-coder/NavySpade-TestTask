using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NavySpade.UI
{
    using Core;

    public class SceneChanger : MonoSingleton<SceneChanger>
    {
        [SerializeField] CanvasGroup canvasGroup = null;
        [Range(0f, 2f)]
        [SerializeField] float duration = 0.5f;

        private string sceneToLoad = string.Empty;
        private string lastScene = string.Empty;

        protected override void Awake()
        {
            base.Awake();

            PlayFadeInAnimation();
        }

        public void FadeToScene(string newScene)
        {
            if (newScene == SceneManager.GetActiveScene().name || string.IsNullOrWhiteSpace(newScene))
                return;

            sceneToLoad = newScene;
            PlayFadeOutAnimation();
        }

        public void BackToPreviosScene()
        {
            sceneToLoad = lastScene;
            PlayFadeOutAnimation();
        }

        public void OnFadeComplete()
        {
            SceneManager.LoadSceneAsync(sceneToLoad).completed += OnSceneLoaded;
        }

        private void PlayFadeInAnimation()
        {
            if (canvasGroup == null)
                return;

            canvasGroup.alpha = 1f;
            canvasGroup.DOFade(0f, duration);
        }

        private void PlayFadeOutAnimation()
        {
            if (canvasGroup == null)
                return;

            canvasGroup.DOFade(1f, duration).OnComplete(() => OnFadeComplete());
        }

        private void OnSceneLoaded(AsyncOperation operation)
        {
            lastScene = sceneToLoad;
            PlayFadeInAnimation();
        }

        private void OnValidate()
        {
            if (canvasGroup == null)
                Debug.LogError($"[{nameof(SceneChanger)}] {nameof(CanvasGroup)} is empty!");
        }
    }
}
