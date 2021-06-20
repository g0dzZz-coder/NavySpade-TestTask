using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NavySpade.UI
{
    using Player;

    public class EndGameMenu : MenuBase
    {
        [SerializeField] private Button restartButton = null;
        [SerializeField] private TMP_Text lastScoreText = null;
        [SerializeField] private TMP_Text bestScoreText = null;

        private void Awake()
        {
            Disable();

            if (restartButton)
                restartButton.onClick.AddListener(OnRestartButtonClicked);

            Level.Instance.GameEnded += Enable;

            var controls = new Controls();
            controls.Hero.Back.Enable();
            controls.Hero.Back.performed += context => Toggle();
        }

        private void Toggle()
        {
            if (menuRoot.gameObject.activeSelf)
                Disable();
            else
                Enable();
        }

        public override void Enable()
        {
            base.Enable();

            var lastScore = Player.Score;
            var bestScore = SaveSystem.GetBestScore();

            lastScoreText.text = lastScore.ToString();
            bestScoreText.text = bestScore.ToString();
        }

        private void OnRestartButtonClicked()
        {
            Disable();

            Level.Instance.Restart();
        }
    }
}