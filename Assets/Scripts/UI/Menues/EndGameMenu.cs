using UnityEngine;

namespace NavySpade.UI
{
    using Core;
    using TMPro;
    using UnityEngine.UI;

    public class EndGameMenu : MenuBase
    {
        [SerializeField] private Button restartButton = null;
        [SerializeField] private TMP_Text scoreText = null;

        private void Awake()
        {
            gameObject.SetActive(false);
            Disable();

            if (restartButton)
                restartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        private void OnEnable()
        {
            Game.Ended += Enable;
            Game.Restarted += Disable;
        }

        private void OnDisable()
        {
            Game.Ended -= Enable;
            Game.Restarted -= Disable;
        }

        private void Enable(bool win)
        {
            base.Enable();

            if (scoreText)
                scoreText.text = "0";
        }

        private void OnRestartButtonClicked()
        {
            Game.Restart();
            Disable();
        }
    }
}