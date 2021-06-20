using UnityEngine;
using UnityEngine.UI;

namespace NavySpade.UI
{
    using Animation;

    public class StartGameMenu : MenuBase
    {
        [Scene]
        [SerializeField] private string gameScene = "SampleLevel";
        [SerializeField] private Button startButton = null;

        private void Start()
        {
            Enable();
            ShowStartButton();

            onEnabled.AddListener(ShowStartButton);
            startButton.onClick.AddListener(OnStartButtonClicked);
        }

        private void OnStartButtonClicked()
        {
            Disable();
            HideStartButton();

            SceneChanger.Instance.FadeToScene(gameScene);
        }

        private void ShowStartButton()
        {
            if (startButton == null)
                return;

            AnimationExtensions.Show(startButton.transform);
        }

        private void HideStartButton()
        {
            AnimationExtensions.Hide(startButton.transform);
        }
    }
}