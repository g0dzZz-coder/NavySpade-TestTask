using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NavySpade.UI
{
    using Core;

    public class EndGameMenu : MenuBase
    {
        [SerializeField] private Button restartButton = null;
        [SerializeField] private TMP_Text scoreText = null;

        private void Awake()
        {
            Disable();

            if (restartButton)
                restartButton.onClick.AddListener(OnRestartButtonClicked);

            var controls = new Controls();
            controls.Hero.Back.Enable();
            controls.Hero.Back.performed += context => Toggle();
        }

        private void OnEnable()
        {
            Game.Ended += Enable;
        }

        private void OnDisable()
        {
            Game.Ended -= Enable;
        }

        private void Toggle()
        {
            if (menuRoot.gameObject.activeSelf)
                Disable();
            else
                Enable();
        }

        private void Enable(bool win)
        {
            base.Enable();

            if (scoreText)
                scoreText.text = "0";
        }

        private void OnRestartButtonClicked()
        {
            Disable();
        }
    }
}