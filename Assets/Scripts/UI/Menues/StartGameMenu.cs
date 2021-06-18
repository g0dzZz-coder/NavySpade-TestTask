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

        private void Awake()
        {
            onEnabled.AddListener(ShowStartButton);
            startButton.onClick.AddListener(OnStartButtonClicked);

            Enable();
            ShowStartButton();
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