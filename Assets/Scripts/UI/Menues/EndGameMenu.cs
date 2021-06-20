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
        }

        public override void Enable()
        {
            lastScoreText.text = Player.Score.ToString();
            bestScoreText.text = SaveSystem.GetBestScore().ToString();

            base.Enable();
        }

        private void OnRestartButtonClicked()
        {
            Disable();
            Level.Instance.Restart();
        }
    }
}