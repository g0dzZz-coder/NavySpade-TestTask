using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NavySpade.UI
{
    using Animation;
    using Core;

    public class StartGameMenu : MenuBase
    {
        [SerializeField] private Button startButton = null;
        [SerializeField] private UnityEvent onGameStarted = null;

        private void Awake()
        {
            onEnabled.AddListener(ShowStartButton);
            startButton.onClick.AddListener(OnStartButtonClicked);

            Enable();
            ShowStartButton();
        }

        private void OnStartButtonClicked()
        {
            HideStartButton();
            Game.Restart();
            Disable();

            onGameStarted?.Invoke();
        }

        private void ShowStartButton()
        {
            if (startButton == null)
                return;

            CustomAnimator.Show(startButton.transform, animationSettings);
        }

        private void HideStartButton()
        {
            CustomAnimator.Hide(startButton.transform, animationSettings);
        }
    }
}