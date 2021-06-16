using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace NavySpade.UI
{
    using Core;
    using UnityEngine.Events;

    public class StartGameMenu : MenuBase
    {
        [SerializeField] private Button startButton = null;
        [SerializeField] private UnityEvent onGameStarted = null;

        private void Awake()
        {
            onEnabled.AddListener(ShowStartButton);
            onDisabled.AddListener(HideStartButton);
            startButton.onClick.AddListener(OnStartButtonClicked);

            Enable();
        }

        private void OnStartButtonClicked()
        {
            Game.Restart();
            Disable();

            onGameStarted?.Invoke();
        }

        private void ShowStartButton()
        {
            startButton.gameObject.SetActive(true);
            startButton.transform.DOJump(Vector3.one, 1f, 1, animationSettings.time);
        }

        private void HideStartButton()
        {
            startButton.gameObject.SetActive(false);
        }
    }
}